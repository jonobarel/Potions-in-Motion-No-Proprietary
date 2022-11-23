using System;
using Internal.UI;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class EngineSpeed : MonoBehaviour, IDisplayable
    {
        private GameSettings _gameSettings;
        
        private EngineFuel _engineFuel;

        public EngineSpeed(EngineFuel engineFuel, GameSettings gameSettings)
        {
            _engineFuel = engineFuel;
            _gameSettings = gameSettings;
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
        
        public float Value()
        {
            return CurrentSpeed();
        }
    }
}