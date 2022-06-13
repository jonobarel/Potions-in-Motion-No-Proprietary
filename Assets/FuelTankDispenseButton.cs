using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace com.baltamstudios.minebuddies
{
    public class FuelTankDispenseButton : ButtonActivated
    {
        public override void TriggerButtonAction(GameObject instigator)
        {
            base.TriggerButtonAction(instigator);
            
            GameSystem.Instance.analytics.LogEvent(instigator.GetComponent<Character>().PlayerID, Analytics.LogAction.DispenseFuel, GameManager.HazardType.A, 1, "player dispensed fuel");
        }
    }
}