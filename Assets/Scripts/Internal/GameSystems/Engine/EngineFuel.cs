using System;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class EngineFuel
    {
        
        public float FuelCapacity
        {
            get;
            private set;
        }

        public float BurnRate
        {
            get;
            private set;
        }
        public float CurrentFuel { get; private set; }
        
        public float FuelLevel => CurrentFuel / FuelCapacity;
       

        public EngineFuel(float capacity, float startingFuel, float burnRate)
        {
            
            if (capacity <= 0f || startingFuel <= 0f || burnRate <= 0f || startingFuel > capacity )
            {
                throw new ArgumentOutOfRangeException("Engine parameters not set correctly");
            }
            
            FuelCapacity = capacity;
            CurrentFuel = startingFuel;
            BurnRate = burnRate;
        }

        public bool HasFuel(float amount)
        {
            return CurrentFuel >= amount;
        }

        public bool RequestFuel(float amount)
        {
            if (HasFuel(amount))
            {
                CurrentFuel -= amount;
                return true;
            }

            return false;
        }
        
        
        
    }
}