using System;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class EngineFuel : IDisplayable
    {
        private GameSettings _gameSettings;
        private bool _ignoreFuel = false;
        public float FuelCapacity
        {
            get;
            private set;
        }

       public float CurrentFuel { get; private set; }
        
        public float FuelLevel => CurrentFuel / FuelCapacity;
       

        public EngineFuel(GameSettings gameSettings, bool ignoreFuel = false)
        {
            _gameSettings = gameSettings;
            _ignoreFuel = ignoreFuel;
            
            FuelCapacity = gameSettings.EngineCapacity;
            CurrentFuel = gameSettings.EngineStartingFuel;
            
            
            if (FuelCapacity <= 0f || CurrentFuel <= 0f || CurrentFuel > FuelCapacity )
            {
                throw new ArgumentOutOfRangeException("Engine parameters not set correctly");
            }
        }

        public bool HasFuel(float amount)
        {
            return (_ignoreFuel || CurrentFuel >= amount);
        }

        public bool RequestFuel(float amount)
        {
            if (_ignoreFuel) return true;
            
            if (HasFuel(amount))
            {
                CurrentFuel -= amount;
                ValueChanged?.Invoke(FuelLevel);
                return true;
            }

            return false;
        }

        public float Value()
        {
            return FuelLevel;
        }

        public event Action<float> ValueChanged;
    }
}