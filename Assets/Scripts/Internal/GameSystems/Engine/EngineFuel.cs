using System;
using Internal.UI;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class EngineFuel : IDisplayable
    {
        private GameSettings _gameSettings;
        public float FuelCapacity
        {
            get;
            private set;
        }

       public float CurrentFuel { get; private set; }
        
        public float FuelLevel => CurrentFuel / FuelCapacity;
       

        public EngineFuel(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            
            FuelCapacity = gameSettings.EngineCapacity;
            CurrentFuel = gameSettings.EngineStartingFuel;
            
            
            if (FuelCapacity <= 0f || CurrentFuel <= 0f || CurrentFuel > FuelCapacity )
            {
                throw new ArgumentOutOfRangeException("Engine parameters not set correctly");
            }
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

        public float Value()
        {
            return FuelLevel;
        }

    }
}