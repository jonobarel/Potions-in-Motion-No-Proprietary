using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class HazardSliderDisplay : MonoBehaviour
    {
        enum State
        {
            Active,
            Close,
            Finished,
            Destroy
        }

        private State _state;
        public Transform iconContainer;
        public Image hazardIcon;

        private Slider _slider;
        private HazardBase _hazard;

        [SerializeField] private Managers.HazardType _type;
        
        public void Start()
        {
            _state = State.Active; 
            _slider = GetComponent<Slider>();
        }
        
        public void Init(HazardBase h)
        {
            _hazard = h;
            _type = h.Type;
            hazardIcon.sprite = GameSystem.Instance.hazardManagerGo.GetIconForHazardType(h.Type);
        }

        public void Update()
        {
            if (_hazard != null)
            {
                _slider.value = 1f - _hazard.Progress;
                if (_hazard.Progress <= 0.25)
                {
                    _state = State.Close;
                }
            }

            if (_state == State.Destroy)
            {
                GameObject.Destroy(gameObject);
            }
            
            
        }

        public void MarkForRemoval()
        {
            _state = State.Destroy;
        }
    }
}