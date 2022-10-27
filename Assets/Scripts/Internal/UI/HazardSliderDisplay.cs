using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class HazardSliderDisplay : MonoBehaviour
    {
        public Transform iconContainer;
        public Image hazardIcon;

        private Slider _slider;
        private HazardBase _hazard;
        
        //TODO: delete this accessor it is superfluous.
        HazardMono _hazardMono;
        public HazardMono HazardMono { 
            set
            {
                if (value != null)
                {
                    
                    var spr = GameSystem.Instance.hazardManagerMono.GetIconForHazardType(value.type);
                    if (spr != null)
                    {
                        hazardIcon.sprite = spr;
                        name = $"Slider {value.name}";
                        _hazardMono = value;
                    }
                    else Debug.LogError($"{name}: could not set hazard icon");
                }
            }
        }

        public void Awake()
        {
            HazardBase.OnExpire += ExpiredHazard;
            HazardBase.OnClear += HazardCleared;
            HazardBase.OnTreat += HazardTreated;
        }

        public void Start()
        {
            _slider = GetComponent<Slider>();
        }
        
        public void Init(HazardBase h)
        {
            _hazard = h;
        }

        private void ExpiredHazard(HazardBase h)
        {
            if (h != _hazard)
            {
                return;
            }
            //TODO: add expired hazard vfx.
            GameObject.Destroy(gameObject);
        }

        private void HazardCleared(HazardBase h)
        {
            if (h != _hazard)
            {
                return;
            }
            
            //TODO: add cleared hazard vfx.
            GameObject.Destroy(gameObject);
        }

        private void HazardTreated(HazardBase h)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            if (_hazard != null)
            {
                _slider.value = 1f - _hazard.Progress;    
            }
        }
    }
}