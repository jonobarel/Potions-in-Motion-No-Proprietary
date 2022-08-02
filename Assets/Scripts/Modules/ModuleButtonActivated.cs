using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class ModuleButtonActivated : ButtonActivated
    {
        ActionModule module;
        public ActionModule Module { get { return module; } }

        public Character instigator;

        public void Start()
        {
            module = GetComponent<ActionModule>();
            if (module == null) Application.Quit();
        }

        public override void TriggerButtonAction(GameObject instigator)
        {
            base.TriggerButtonAction(instigator);
            this.instigator = instigator.GetComponent<Character>();
            GetComponent<ActionModule>().ActivatingPlayer = instigator.GetComponent<MineBuddiesCharacter>().PlayerLabel;
            GameSystem.Instance.analytics.LogEvent(this.instigator.GetComponent<MineBuddiesCharacter>().PlayerLabel, Analytics.LogAction.UseModule, module.hazardType, 1, "player activated module"); 
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}