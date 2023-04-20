using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class MineBuddiesButtonActivated : ButtonActivated, IModuleActivation
    {
        private Module _module;
        
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

        [Inject]
        private GameSettings _gameSettings;

        [MMInspectorGroup("Visual Prompt", true, 15)]
        public bool ShowPromptWhenNotActivable = false;

        private float _treatmentAmount;
        public HazardManagerGO.InteractionType InteractionType => HazardManagerGO.InteractionType.Button;

        private void Awake()
        {
            _module = GetComponent<Module>();
        }

        public override void Initialization()
        {
            base.Initialization();
            if (_buttonPrompt == null)
            {
                _buttonPrompt = (ButtonPrompt)Instantiate(ButtonPromptPrefab);
                _buttonPrompt.Initialization();
                _buttonPromptAnimator = _buttonPrompt.gameObject.MMGetComponentNoAlloc<Animator>();
            }
        }
        
        protected override void ActivateZone()
        {
            base.ActivateZone();
            if (_module != null)
            {
                _module.Interact(_treatmentAmount);
            }
        }

        public override void ShowPrompt()
        {
            if (ShowPromptWhenNotActivable || Activable)
            {
                base.ShowPrompt();
            }
        }

        public override void ToggleActivable()
        {
            base.ToggleActivable();
            if (!Activable && !ShowPromptWhenNotActivable)
            {
                HidePrompt();
            }
        }
    }
}