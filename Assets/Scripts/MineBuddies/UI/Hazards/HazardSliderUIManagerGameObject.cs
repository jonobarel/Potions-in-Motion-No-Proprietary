using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.MineBuddies;
using ZeroPrep.UI;
using UnityEngine.UI;
using Zenject;


namespace ZeroPrep.MineBuddies
{
    public class HazardSliderUIManagerGameObject : MonoBehaviour
    {
        /// <summary>
        /// This is the MonoBehaviour script attached to the HazardTimeline UI.
        /// It handles creating the Slider manager which instantiates the Hazard sliders
        /// and providing access to scene elements.
        /// </summary>
        // Start is called before the first frame update

        [SerializeField] private GameObject _hazardQueueItemPrefab;
        [SerializeField] private QueueContainer _queueContainer;
        public GameObject HazardQueueItemPrefab => _hazardQueueItemPrefab;


        private Dictionary<HazardBase, HazardDisplay> _queueItems;

        [Inject] private HazardIcons _hazardIcons;

        void Start()
        {
            _queueItems = new Dictionary<HazardBase, HazardDisplay>();
            HazardBase.Spawn += OnSpawn;
            HazardBase.Clear += OnClear;
            HazardBase.Expire += OnExpire;
            HazardBase.Treat += OnTreat;
            HazardBase.Advance += OnAdvance;
        }



        void OnDestroy()
        {
            HazardBase.Spawn -= OnSpawn;
            HazardBase.Clear -= OnClear;
            HazardBase.Expire -= OnExpire;
            HazardBase.Treat -= OnTreat;
        }

        void OnTreat(HazardBase h)
        {
            if (_queueItems.ContainsKey(h))
            {
                _queueItems[h].Treat();
            }
            else throw new ArgumentOutOfRangeException("Could not find HazardDisplay object for HazardID " + h.ID);
        }

        private void OnSpawn(HazardBase h)
        {
            AddHazardToTimeline(h);
        }

        private void AddHazardToTimeline(HazardBase h)
        {
            HazardDisplay queueItem = GameObject.Instantiate(HazardQueueItemPrefab, _queueContainer.transform)
                .GetComponent<HazardDisplay>();
            queueItem.Init(h, _hazardIcons.GetIconForHazardType(h.Type));
            _queueContainer.AddObjectToQueue(queueItem.GetComponent<RectTransform>(), false);
            _queueItems.Add(h, queueItem);
            queueItem.name = $"Hazard: {h.ID}";
        }

        private void OnExpire(HazardBase h)
        {
            PlayExpiredAnimation(h);
            RemoveQueueIcon(h);

        }

        private void PlayExpiredAnimation(HazardBase hazardBase)
        {
            throw new NotImplementedException();
        }


        private void OnClear(HazardBase h)
        {
            PlayClearedAnimation(h);
            RemoveQueueIcon(h);
        }

        
        private void PlayClearedAnimation(HazardBase h)
        {
            if (_queueItems.TryGetValue(h, out var item))
            {
                item.Clear();
            }
            else throw new ArgumentOutOfRangeException("Could not find HazardDisplay object for HazardID " + h.ID);
        }

        private void RemoveQueueIcon(HazardBase h)
        {
            if (_queueItems.Remove(h, out var removedItem) && removedItem)
            {
                removedItem.MarkForRemoval();
                Destroy(removedItem.gameObject, 1f);
            }
            else
            {
                throw new ArgumentException($"Could not find slider for hazard {h.ID}");
            }
        }
        
        private void OnAdvance(HazardBase obj)
        {
            throw new NotImplementedException();
        }

    }
}