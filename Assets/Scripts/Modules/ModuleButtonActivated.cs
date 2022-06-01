using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace com.baltamstudios.minebuddies
{
    public class ModuleButtonActivated : ButtonActivated
    {
        ActionModule module;

        public void Start()
        {
            module = GetComponent<ActionModule>();
            if (module == null) Application.Quit();
        }

        public override void TriggerButtonAction(GameObject instigator)
        {
            base.TriggerButtonAction(instigator);
            GameSystem.Instance.analytics.LogEvent(instigator.name, Analytics.LogAction.UseModule, module.hazardType, 1, "player activated module"); 
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}