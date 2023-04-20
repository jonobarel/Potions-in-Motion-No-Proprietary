using UnityEngine.Events;

namespace ZeroPrep.MineBuddies
{
    public class MineBuddiesRotationActivated : RotationActivated, IModuleActivation
    {
        public void ToggleStatus()
        {
            ToggleActivable();
        }

        private Module _module;

        void Awake()
        {
            _module = GetComponent<Module>();
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
                _module.Interact(angle);
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