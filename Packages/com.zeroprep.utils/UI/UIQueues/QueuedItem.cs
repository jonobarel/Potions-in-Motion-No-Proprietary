using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace ZeroPrep.UI
{

    [RequireComponent(typeof(RectTransform))]
    public class QueuedItem : MonoBehaviour
    {
   
        [SerializeField] public Vector2 targetPosition;
        [SerializeField] public QueuedItem Prev  {get; private set;}
        
        [SerializeField] public RectTransform rectTransform;
        
        
        internal static UnityEvent<QueuedItem> ObjectRemovedFromQueue = new UnityEvent<QueuedItem>();
        private Vector2 DistanceToTarget => rectTransform.anchoredPosition - targetPosition;
        private Vector2 SizeDelta => rectTransform ? rectTransform.sizeDelta : Vector2.zero;

        QueueContainer _container;
        
        private QueueContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = GetComponentInParent<QueueContainer>();
                }

                return _container;
            }
        }       
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

        public void SetNewTarget(QueuedItem newPrev) {
            Prev = newPrev;
        }

        public void SlideToPosition(float delay = 0f, float duration = 0.5f) 
        {
            targetPosition = (Prev != null) ? Prev.targetPosition + (Prev.SizeDelta.x + Container._queueSpacing*Prev.SizeDelta.x) * Vector2.right : Container.RectTransform.anchoredPosition;
            DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, targetPosition, duration).SetEase(Container.easingType, Container.easingParameter);
        }

    }
}
