using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ZeroPrep.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class QueueContainer : MonoBehaviour
    {
        [SerializeField] private List<QueuedItem> _queuedObjects = new List<QueuedItem>();
        [SerializeField] public float _queueSpacing = 0.5f;
        [SerializeField] private float _slideRate = 0.5f;
        [SerializeField] private float startingOffset = 500;
        [SerializeField] private float slideLatency = 0.1f;
        [SerializeField] private float firstObjectOffset = 0.5f;
        public bool allItemsIdenticalWidth = true;
        private RectTransform _rectTransform;

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }
        private float _slidingDistanceEpsilon = 0.1f;
        public float SlideRate => _slideRate;

        public void AddObjectToQueue(RectTransform newObject, bool slideToPosition = true)
        {

            Vector2 startPosition = _rectTransform.anchoredPosition + startingOffset * Vector2.right;
            newObject.transform.SetParent(transform);
            newObject.anchoredPosition = startPosition;
            QueuedItem item = SetupObjectForInsertion(newObject);
            _queuedObjects.Add(item);
            if (slideToPosition)
            {
                item.SlideToPosition();
            }
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            QueuedItem.ObjectRemovedFromQueue.AddListener(HandleItemRemoval);


        }

        private void HandleItemRemoval(QueuedItem item)
        {
            //locate the item to remove
            int index = _queuedObjects.IndexOf(item);
            _queuedObjects.RemoveAt(index);
            QueuedItem[] itemsToSlide = _queuedObjects.GetRange(index, _queuedObjects.Count - index).ToArray();
            QueuedItem prev = item.Prev;
            foreach (var t in itemsToSlide)
            {
                t.SetNewTarget(prev);
                prev = t;
            }
          
        }


        void OnDisable()
        {
            QueuedItem.ObjectRemovedFromQueue.RemoveListener(HandleItemRemoval);
        }

        private QueuedItem SetupObjectForInsertion(RectTransform newItem)
        {
            QueuedItem item = newItem.gameObject.AddComponent<QueuedItem>();
            item.SetNewTarget(_queuedObjects.Count() > 0 ? _queuedObjects.Last() : null);
            return item;
        }
        
    }
}