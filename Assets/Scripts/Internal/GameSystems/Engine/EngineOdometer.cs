using Internal.UI;
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
        }

        public float Value()
        {
            return DistanceCovered;
        }
    }
}