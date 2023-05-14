using UnityEngine.Events;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class MineBuddiesRotationActivated : RotationActivated, IModuleActivation
    {
        public void ToggleStatus()
        {
            ToggleActivable();
        }

        private Module _module;
        
        [Inject]
        private GameSettings _gameSettings;

        private float _treatmentAmount;
        void Awake()
        {
            _module = GetComponent<Module>();
            _treatmentAmount = _gameSettings.TreatmentEffect;
        }
        public new bool Activable { get => base.Activable;
            set
            {
                base.Activable = value;
                if (!Activable && !ShowPromptWhenNotActivable)
                {
                    HidePrompt();
                }
            }
        }
        
        public HazardManagerGO.InteractionType InteractionType => HazardManagerGO.InteractionType.Rotation;

        protected override void ActivateZone(float angle)
        {
            base.ActivateZone(angle);
            if (_module != null)
            {
                _module.Interact(angle/360*_treatmentAmount);
            }
        } 
        
        public override void ShowPrompt()
        {
            if (ShowPromptWhenNotActivable || Activable)
            {
                base.ShowPrompt();
            }
        }

    }
}