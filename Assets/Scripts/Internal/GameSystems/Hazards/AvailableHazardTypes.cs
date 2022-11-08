using System.Collections.Generic;
using UnityEngine;
namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// This class exposes the list of available hazard types (derived from active modules in the current session.
    /// </summary>
    public class AvailableHazardTypes
    {
        public Managers.HazardType[] Types { get; private set; }
        
        public AvailableHazardTypes()
        {
            ActionModule[] modules = GameObject.FindObjectsOfType<ActionModule>();
            
            if (modules.Length < 1)
            {
                throw new System.ArgumentOutOfRangeException("Cannot find Action Modules in scene");
            }

            List<Managers.HazardType> hazardTypes = new List<Managers.HazardType>();
            
            foreach (var module in modules)
            {
                hazardTypes.Add(module.hazardType);                
            }

            Types = hazardTypes.ToArray();
        }
        
    }
}