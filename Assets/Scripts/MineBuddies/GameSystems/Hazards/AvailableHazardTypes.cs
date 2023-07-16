using System;
using System.Collections.Generic;
using System.Linq;
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
            Module[] modules = GameObject.FindObjectsOfType<Module>();
            
            if (modules.Length < 1)
            {
                Types = Enum.GetValues(typeof(Managers.HazardType)) as Managers.HazardType[];
            }
            else
            {
                Types = modules.Where(m => m.gameObject.activeInHierarchy).Select(module => module.HazardType).ToArray();
            }
        }
        
    }
}