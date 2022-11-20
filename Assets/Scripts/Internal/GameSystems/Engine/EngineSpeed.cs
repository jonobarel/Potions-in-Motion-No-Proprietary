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

        private float _maxSpeed;
        private float _acceleration;
        /// <summary>
        /// Amount of fuel consumed/second of engine operation.
        /// </summary>
        private float _burnRate;
        
        [Inject]
        public void Init(EngineFuel engineFuel, GameSettings gameSettings)
        {
            _engineFuel = engineFuel;
            _gameSettings = gameSettings;
            _currentSpeed = 0f;
            _maxSpeed = gameSettings.EngineMaxSpeed;
            _acceleration = gameSettings.EngineAcceleration;
            _burnRate = gameSettings.EngineBurnRate;

        }

        public float CurrentSpeed()
        {
            //TODO - calculate speed function for EngineSpeed.
            return _currentSpeed;
        }

        public void Update()
        {
            if (_engineFuel.RequestFuel(_burnRate * Time.deltaTime))
            {
                _currentSpeed += Time.deltaTime * _acceleration;
                
            }
            else
            {
                _currentSpeed -= Time.deltaTime * _acceleration;
            }
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _maxSpeed);
            
        }
    }
}