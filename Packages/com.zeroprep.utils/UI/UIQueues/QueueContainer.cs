using System;
using System.Collections;
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
        [SerializeField] private float _queueSpacing = 0.5f;
        [SerializeField] private float _slideRate = 0.5f;
        [SerializeField] private float startingOffset = 500;
        [SerializeField] private float slideLatency = 0.1f;
        [SerializeField] private float firstObjectOffset = 0.5f;
        public bool allItemsIdenticalWidth = true;
        private RectTransform _rectTransform;
        private float _slidingDistanceEpsilon = 0.1f;
        public float SlideRate => _slideRate;

        public void AddObjectToQueue(RectTransform newObject)
        {

            Vector2 startPosition = _rectTransform.anchoredPosition + startingOffset * Vector2.right;
            QueuedItem item = SetupObjectForInsertion(newObject);
            _queuedObjects.Add(item);
            newObject.transform.SetParent(transform);
            newObject.anchoredPosition = startPosition;
            SlideToPosition(item);
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            QueuedItem.ObjectRemovedFromQueue.AddListener(HandleItemRemoval);
        }

        private void SlideToPosition(QueuedItem item, float delay = 0f)
        {
            if (this.isActiveAndEnabled && gameObject.activeInHierarchy)
            {
                StartCoroutine(SlidingCoroutine(item, delay));
            }
                    
        }

        private Vector2 CalculateTargetPosition(RectTransform newItem)
        {
            int count = _queuedObjects.Count;
            float width = newItem.sizeDelta.x;

            if (count == 0)
            {
                return _rectTransform.anchoredPosition + width*firstObjectOffset * Vector2.right;
            }
            
            return _queuedObjects.Last().targetPosition+(_queuedObjects.Last().SizeDelta.x+_queueSpacing)*Vector2.right;
        }

        private void HandleItemRemoval(QueuedItem item)
        {
            //locate the item to remove
            int index = _queuedObjects.IndexOf(item);
            _queuedObjects.RemoveAt(index);
            List<QueuedItem> itemsToSlide = _queuedObjects.GetRange(index, _queuedObjects.Count - index);
            QueuedItem prev = item;
            int i = 0;
            foreach (var queuedItem in itemsToSlide)
            {
                queuedItem.targetPosition+=(prev.SizeDelta.x+_queueSpacing)*Vector2.left;
                SlideToPosition(queuedItem, i++ * slideLatency);
                prev = queuedItem;
            }
        }

        void OnDisable()
        {
            QueuedItem.ObjectRemovedFromQueue.RemoveListener(HandleItemRemoval);
        }

        private IEnumerator SlidingCoroutine(QueuedItem item, float delayStart = 0f)
        {
            float targetX = item.targetPosition.x;
            RectTransform rectTransform = item.rectTransform;

            if (delayStart > 0)
            {
                yield return new WaitForSeconds(delayStart);
            }

            
            while (!(Mathf.Abs(targetX-item.rectTransform.anchoredPosition.x) <= _slidingDistanceEpsilon))
            {
                rectTransform.anchoredPosition+=new Vector2(_slideRate*Time.deltaTime*(targetX-rectTransform.anchoredPosition.x),0);
                yield return null;
            }
            item.rectTransform.anchoredPosition = item.targetPosition;
        }

        private QueuedItem SetupObjectForInsertion(RectTransform newItem)
        {
            QueuedItem item = newItem.gameObject.AddComponent<QueuedItem>();
            item.targetPosition = CalculateTargetPosition(newItem);
            return item;
        }
        
    }
}