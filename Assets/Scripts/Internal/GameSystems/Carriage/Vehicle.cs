using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class Vehicle : MonoBehaviour
    {
        private EngineSpeed _engineSpeed;
        private EngineFuel _engineFuel;
        private EngineOdometer _engineOdometer;

        [Inject]
        private void Init(EngineSpeed engineSpeed, EngineFuel engineFuel, EngineOdometer engineOdometer)
        {
            _engineFuel = engineFuel;
            _engineSpeed = engineSpeed;
            _engineOdometer = engineOdometer;
        }
        
        public void Update() {
            _engineOdometer.Update(Time.deltaTime);
        }
        
        
    }
}