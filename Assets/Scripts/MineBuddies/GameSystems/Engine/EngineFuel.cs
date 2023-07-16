using System;
using DG.Tweening;
using UnityEditor.UIElements;
using UnityEngine;
using Zenject;
using ZeroPrep.UI;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class EngineFuel : IDisplayable
    {
        [SerializeField] private float fuelReloadDuration = 0.5f; 
        private GameSettings _gameSettings;
        private bool _ignoreFuel = false;
        private Tweener refuellingTweener;
        
        public float FuelCapacity
        {
            get;
            private set;
        }

       public float CurrentFuel { get; private set; }
        
        public float FuelLevel => CurrentFuel / FuelCapacity;

        public void Refuel(float amount)
        {
            CurrentFuel = Mathf.Clamp(CurrentFuel + amount, 0, FuelCapacity);
            ValueChanged?.Invoke(FuelLevel);
        }
        [Inject]
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