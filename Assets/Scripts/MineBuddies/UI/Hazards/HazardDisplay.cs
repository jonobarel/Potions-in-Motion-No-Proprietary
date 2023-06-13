using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ZeroPrep.MineBuddies
{
    public class HazardDisplay : MonoBehaviour
    {
        enum State
        {
            Active,
            Close,
            Finished,
            Destroy
        }

        [Tooltip("The duration of the tween when the hazard is treated")]
        public float tweenDuration = 0.2f;

        [SerializeField]
        private MMF_Player _treated_feedback, _expired_feedback, _cleared_feedback;
        
        private State _state;
        public Transform iconContainer;
        public Image hazardIcon;
        public Image treatmentProgressDisplay;


        private HazardBase _hazard;
        public HazardBase Hazard => _hazard;
        
        [SerializeField] private Managers.HazardType _type;
        
        public void Start()
        {
            _state = State.Active; 
        }

        public void Init(HazardBase h, Sprite icon)
        {
            _hazard = h;
            _type = h.Type;
            hazardIcon.sprite = icon;
        }

        public void Update()
        {
            if (_state == State.Destroy)
            {
                return;
            }
            
            if (_hazard != null)
            {
                if (_hazard.Progress <= 0.25)
                {
                    _state = State.Close;
                }
            }

        }

        public void MarkForRemoval()
        {
            _state = State.Destroy;
        }

        public void Treat()
        {
            SetTreatmentDisplay();
            _treated_feedback.PlayFeedbacks();
        }

        public void Clear()
        {
            _cleared_feedback.PlayFeedbacks();
        }

        private void SetTreatmentDisplay()
        {
            DOTween.To(() => treatmentProgressDisplay.fillAmount, x => treatmentProgressDisplay.fillAmount = x, 1 - _hazard.Health, tweenDuration);
        }

    }
    

}