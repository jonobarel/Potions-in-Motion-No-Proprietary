using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace ZeroPrep.MineBuddies
{
    public class FuelTankDispenseButton : MineBuddiesButtonActivated
    {
        public override void TriggerButtonAction(GameObject instigator)
        {
            base.TriggerButtonAction(instigator);
            
            GameSystem.Instance.analytics.LogEvent(instigator.GetComponent<MineBuddiesCharacter>().PlayerLabel, Analytics.LogAction.DispenseFuel, Managers.HazardType.A, 1, "player dispensed fuel");
        }
    }
}