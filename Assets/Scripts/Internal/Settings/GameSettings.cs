using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class GameSettings
    {
        [Header("Engine Properties")]
        public float EngineCapacity;

        public float EngineStartingFuel;

        public float EngineBurnRate;
        
        [Header("Module Settings")]
        public float FuelConsumption;

        [Tooltip("base value of how much the module affects the target hazards, before buffs/debuffs")]
        [Range(0f, 1f)]
        public float TreatmentEffect;

        [Header("Hazard Settings")] 
        [Range(0f,20f)]
        public float MinSpawnTime;

        [Range(0f, 20f)] public float MaxSpawnTime;
        public float HazardStartingHealth;


    }
}