using System.Collections;
using System.Collections.Generic;
using DG.DemiEditor.DeGUINodeSystem;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class PowerModule : Module
    {
        [SerializeField] private float refuelFactor;
        /// <summary>
        /// Adds fuel <c>amount</c> to engine fuel.
        /// </summary>
        /// <param name="amount">amount of energy to add to the <c>_engineFuel</c> component</param>
        public override void Interact(float amount)
        {
            _engineFuel.Refuel(amount*refuelFactor);
        }

        protected override void ToggleHazardInteractions(Managers.HazardType type,
            HazardManagerGO.InteractionType interactions)
        {
            return;
        }
    }
}