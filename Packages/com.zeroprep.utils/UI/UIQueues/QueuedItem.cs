using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZeroPrep.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class QueuedItem : MonoBehaviour
    {
        [SerializeField] public Vector2 targetPosition;
        [SerializeField] public RectTransform rectTransform;

        
        internal static UnityEvent<QueuedItem> ObjectRemovedFromQueue = new UnityEvent<QueuedItem>();
        private Vector2 DistanceToTarget => rectTransform.anchoredPosition - targetPosition;
        public Vector2 SizeDelta => rectTransform ? rectTransform.sizeDelta : Vector2.zero;

       
        void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        void OnDisable()
        {
            NotifyQueueContainerForRemoval();
        }
        
        private void NotifyQueueContainerForRemoval()
        {
            if (ObjectRemovedFromQueue != null)
            {
                ObjectRemovedFromQueue.Invoke(this);
            }
        }


    }
}
