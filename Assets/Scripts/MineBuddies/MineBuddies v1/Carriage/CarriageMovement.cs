using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZeroPrep.MineBuddies
{
    public class CarriageMovement
    {
        ConfigManager _configManager;
        float _distanceCovered = 0f;
        public float DistanceCovered { get { return _distanceCovered; } }
        [SerializeField]
        [Range(0f, 10f)]
        float _speedOverride;
        float _currentSpeed;
        TextMeshProUGUI _odometer;
        TextMeshProUGUI _speedometer;
        bool _brake = false;

        public void ToggleBrake()
        {
            _brake = !_brake;
        }

        public float CurrentSpeed { get { return _currentSpeed; } }
        float MaxSpeed => _configManager.config.MaxCarriageSpeed;

        float _hazardSlowdownFactor = 0.1f;

        //for Lerping speed.
        float _startSpeed;
        float _targetSpeed;
        float _speedChangeProgress = 0f;


        float CalculateTargetSpeed()
        {
           /* if (_speedOverride > 0f)
                return _speedOverride;
            //let's set the speed as MaxSpeed - A*activeHazards*(1-damage)
            else if (VehicleDamageHandler.Instance.Engine.FuelLevel > 0)
            {
                // max speed is reduced by damage ratio
                // speed is reduced by number of active hazards * slowdown factor 
                var speed = MaxSpeed * (1 - VehicleDamageHandler.Instance.CurrentDamage) - _hazardSlowdownFactor * GameSystem.HazardManagerGo.ActiveHazardsCount();
                speed = Mathf.Clamp(speed, 0, MaxSpeed);
                return speed;
            }
            else return 0f;*/
           return 1f;

        }

        public CarriageMovement(TextMeshProUGUI odo, TextMeshProUGUI speedo)
        {
            _configManager = GameObject.FindObjectOfType<ConfigManager>();
            _speedOverride = 0f;
            _speedometer = speedo;
            _odometer = odo;

        }

        // Update is called once per frame
        public void Tick(float deltaTime)
        {
            float newTargetSpeed = 0f;
            if (!_brake)
                newTargetSpeed = CalculateTargetSpeed();

            if (newTargetSpeed > _targetSpeed || newTargetSpeed < _targetSpeed) //need to restart the speed shifting process
            {
                _speedChangeProgress = 0f;
                _targetSpeed = newTargetSpeed;
                _startSpeed = _currentSpeed;
            }

            if ((_currentSpeed - _targetSpeed)*(_currentSpeed - _targetSpeed) > 0.01) //Not yet at target speed
            {
                _currentSpeed = Mathf.Lerp(_startSpeed, _targetSpeed, _speedChangeProgress);
                _speedChangeProgress += deltaTime;
                if (_speedChangeProgress >= 1f) _currentSpeed = _targetSpeed;
            }

            _distanceCovered += _currentSpeed * deltaTime;
            
            //transform.position += new Vector3(currentSpeed * Time.deltaTime,0,0);
            if (_odometer != null)
            {
                _odometer.SetText($"{(int)_distanceCovered}");
            }
            if (_speedometer != null)
            {
                _speedometer.SetText($"{_currentSpeed:F} m/s");
            }
        }

    }
}