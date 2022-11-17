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
            Module[] modules = GameObject.FindObjectsOfType<Module>();
            
            if (modules.Length < 1)
            {
                Types = new[] { Managers.HazardType.A, Managers.HazardType.B, Managers.HazardType.C };
            }
            else
            {
                List<Managers.HazardType> hazardTypes = new List<Managers.HazardType>();

                foreach (var module in modules)
                {
                    hazardTypes.Add(module.HazardType);
                }

                Types = hazardTypes.ToArray();
            }
        }
        
    }
}