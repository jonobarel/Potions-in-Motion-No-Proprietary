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
        public TextMeshProUGUI Odometer;
        public TextMeshProUGUI Speedometer;
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


        float CalculateSpeed()
        {
            if (_speedOverride > 0f)
                return _speedOverride;
            //let's set the speed as MaxSpeed - A*activeHazards*(1-damage)
            else if (Carriage.Instance.Engine.FuelLevel > 0)
            {
                var speed = MaxSpeed * (1 - Carriage.Instance.CurrentDamage) - _hazardSlowdownFactor * GameSystem.HazardManager.ActiveHazards.Count;
                speed = Mathf.Clamp(speed, 0, MaxSpeed);
                return speed;
            }
            else return 0f;
            
        }

        public CarriageMovement()
        {
            _configManager = GameObject.FindObjectOfType<ConfigManager>();
            _speedOverride = 0f;
            #region config
            _hazardSlowdownFactor = _configManager.config.HazardSlowDownFactor;
            #endregion

        }

        // Update is called once per frame
        public void Tick(float deltaTime)
        {
            float newTargetSpeed = 0f;
            if (!_brake)
                newTargetSpeed = CalculateSpeed();

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
            if (Odometer != null)
            {
                Odometer.SetText($"{(int)_distanceCovered}");
            }
            if (Speedometer != null)
            {
                Speedometer.SetText($"{_currentSpeed:F} m/s");
            }
        }

    }
}