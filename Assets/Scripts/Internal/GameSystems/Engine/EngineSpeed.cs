using System;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class EngineSpeed : MonoBehaviour
    {
        private GameSettings _gameSettings;
        
        [SerializeField]
        private EngineFuel _engineFuel;

        [SerializeField]
        private float _currentSpeed;

        [SerializeField]
        private float _targetSpeed;

        public float TargetSpeed => _targetSpeed;
        
        private float _maxSpeed;
        private float _acceleration;
        private float _deceleration;
        
        /// <summary>
        /// Amount of fuel consumed/second of engine operation.
        /// </summary>
        private float _burnRate;

        private HazardManager _hazardManager;
        
        [Inject]
        public void Init(EngineFuel engineFuel, GameSettings gameSettings, HazardManager hazardManager)
        {
            if (gameSettings.EngineDeceleration <= 0f)
            {
                throw new System.ArgumentOutOfRangeException("Deceleration should be > 0");
            }

            if (gameSettings.EngineDeceleration <= 0f)
            {
                throw new System.ArgumentOutOfRangeException("Acceleration should be > 0");
            }
            _engineFuel = engineFuel;
            _gameSettings = gameSettings;
            _currentSpeed = 0f;
            _maxSpeed = gameSettings.EngineMaxSpeed;
            _acceleration = gameSettings.EngineAcceleration;
            _deceleration = gameSettings.EngineDeceleration;
            _burnRate = gameSettings.EngineBurnRate;
            _hazardManager = hazardManager;

        }

        public float CurrentSpeed()
        {
            //TODO - calculate speed function for EngineSpeed.
            return _currentSpeed;
        }

        public float CalculateTargetSpeed()
        {
            float hazardsMax = (float)_gameSettings.MaxHazardsForSlowdown;

            float hazardsFactor = Mathf.Min(_hazardManager.Hazards.Count / hazardsMax,1f );
            
            float maxPotential =  _maxSpeed * (1-_gameSettings.HazardSlowdownFactor * hazardsFactor);

            return maxPotential;
        }

        public void Update()
        {
            CalculateCurrentSpeed();
        }

        private void CalculateCurrentSpeed()
        {
            if (_engineFuel.RequestFuel(Time.deltaTime*_burnRate))
            { 
                _targetSpeed = CalculateTargetSpeed();
            }
            else //we're out of fuel!
            {
                _targetSpeed = 0;
            }

            if (_targetSpeed > _currentSpeed)
            {
                _currentSpeed += _acceleration * Time.deltaTime;
                _currentSpeed = Mathf.Min(_targetSpeed, _currentSpeed);
            }
            else
            {
                _currentSpeed -= _deceleration * Time.deltaTime;
                _currentSpeed = Mathf.Max(_targetSpeed, _currentSpeed);
            }
        }
    }
}