using System;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class EngineOdometer : IDisplayable
    {
        private EngineSpeed _engineSpeed;
        public float DistanceCovered { get; private set; }
        
        [Inject]
        public EngineOdometer(EngineSpeed engineSpeed)
        {
            _engineSpeed = engineSpeed;
        }

        public void Update(float deltaTime)
        {
            DistanceCovered += _engineSpeed.CurrentSpeed() * deltaTime;
            ValueChanged?.Invoke(DistanceCovered);
        }

        public float Value()
        {
            return DistanceCovered;
        }

        public event Action<float> ValueChanged;
    }
}