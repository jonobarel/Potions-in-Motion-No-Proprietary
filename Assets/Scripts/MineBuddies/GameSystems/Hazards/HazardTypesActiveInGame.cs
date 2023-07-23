using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// This class exposes the list of available hazard types (derived from active modules in the current session.
    /// </summary>
    public class HazardTypesActiveInGame
    {
        public HazardType[] Types { get; private set; }
        
        public HazardTypesActiveInGame()
        {
            Module[] modules = GameObject.FindObjectsOfType<Module>();
            
            if (modules.Length < 1) //no modules found in scene, get just all the types now.
            {
                Types = HazardType.AvailableTypes.ToArray();
            }
            else
            {
                Types = modules.Where(m => m.gameObject.activeInHierarchy).Select(module => module.HazardType).ToArray();
            }
        }
        
    }
}