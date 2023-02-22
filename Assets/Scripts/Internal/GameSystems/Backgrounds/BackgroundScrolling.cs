
using UnityEngine;
using System.Linq;
using Zenject;
using System;

namespace ZeroPrep.MineBuddies
{
    public class BackgroundScrolling : MonoBehaviour
    {
        private EngineSpeed _engineSpeed;

        public event Action<float> SpeedChanged;
        
        [Inject]
        void Init(EngineSpeed engineSpeed)
        {
            _engineSpeed = engineSpeed;
        }
        
        void Start()
        {
            if (_engineSpeed is not null)
            {
                _engineSpeed.ValueChanged += OnValueChanged;
            }
        }

        public void OnDestroy()
        {
            if (_engineSpeed != null)
            {
                _engineSpeed.ValueChanged -= OnValueChanged;
            }
        }

        void OnValueChanged(float value)
        {
            if (SpeedChanged != null)
            {
                SpeedChanged.Invoke(value/_engineSpeed.MaxSpeed);
            }
        }
    }
}