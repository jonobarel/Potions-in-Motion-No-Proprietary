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

        [Tooltip("in m/s^2")]
        public float EngineAcceleration;
        public float EngineDeceleration;

        public float EngineMaxSpeed;

        [Header("Vehicle Properties")] public int VehicleStartingHealth;
        public int BaseDamageOnHazardExpire;
        public float DamageFlickerDuration;
        public float InvulnerabilityDuration;
       
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

        public float HazardMinSpeed;
        public float HazardSpeedToEngineRatio;

        [Tooltip("This number is used to calculate how the number of hazards affects the speed of the vehicle")]
        public float HazardSlowdownFactor;
        public int MaxHazardsForSlowdown;


        [Header("General")]
        [Tooltip("Seconds countdown when all players are ready on Player Join screen")]
        public int PlayerReadyDelay;

    }
}