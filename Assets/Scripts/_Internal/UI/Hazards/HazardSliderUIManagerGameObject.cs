using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.MineBuddies;
using UnityEngine.UI;
using Zenject;


namespace ZeroPrep.MineBuddies
{
    public class HazardSliderUIManagerGameObject : MonoBehaviour
    {
        /// <summary>
        /// This is the MonoBehaviour script attached to the HazardTimeline UI.
        /// It handles creating the Slider manager which instantiating the Hazard sliders
        /// and providing access to scene elements.
        /// </summary>
        // Start is called before the first frame update

        [SerializeField] private Slider _hazardProgressDisplayPrefab;

        public Slider ProgressSliderPrefab => _hazardProgressDisplayPrefab;

        [SerializeField] private Transform _hazardSliderContainer;

        public Transform Slidercontainer => _hazardSliderContainer;

        //private HazardSliderUIManager _hazardSliderUIManager;
        private Dictionary<HazardBase, HazardSliderDisplay> _sliders;

        [Inject] private HazardIcons _hazardIcons;

        void Start()
        {
            _sliders = new Dictionary<HazardBase, HazardSliderDisplay>();
            HazardBase.Spawn += OnSpawn;
            HazardBase.Clear += OnClear;
            HazardBase.Expire += OnExpire;
            HazardBase.Treat += OnTreat;
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
            _sliders[h].TreatmentAnimation();
        }

        private void OnSpawn(HazardBase h)
        {
            AddHazardToTimeline(h);
        }

        private void AddHazardToTimeline(HazardBase h)
        {
            //Instantiate the UI element and
            //add it to the timeline

            Transform sliderContainer = Slidercontainer;
            Slider prefab = ProgressSliderPrefab;

            Slider positionSlider = GameObject.Instantiate(prefab, sliderContainer);
            _sliders.Add(h, positionSlider.GetComponent<HazardSliderDisplay>());
            positionSlider.name = $"Hazard: {h.ID}";
            positionSlider.GetComponent<HazardSliderDisplay>().Init(h, _hazardIcons.GetIconForHazardType(h.Type));
        }

        private void OnExpire(HazardBase h)
        {
            HazardExpired(h);
        }

        private void HazardExpired(HazardBase h)
        {
            HardRemoveSliderFromTimeline(h);
        }

        private void OnClear(HazardBase h)
        {
            _sliders[h].PlayClearAnimation();
            HazardCleared(h);
        }

        private void HazardCleared(HazardBase h)
        {
            HardRemoveSliderFromTimeline(h);
        }

        private void HardRemoveSliderFromTimeline(HazardBase h)
        {
            HazardSliderDisplay positionSlider;
            if (_sliders.Remove(h, out positionSlider) && positionSlider)
            {
                positionSlider.GetComponent<HazardSliderDisplay>().MarkForRemoval();
            }
            else
            {
                throw new ArgumentException($"Could not find slider for hazard {h.ID}");
            }
        }


    }
}