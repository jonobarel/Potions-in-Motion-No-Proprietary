using MoreMountains.CorgiEngine;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ModulePlacement))]
    [RequireComponent(typeof(PlatformPlacement))]
    public class Vehicle : MonoBehaviour
    {
        private EngineSpeed _engineSpeed;
        // ReSharper disable once NotAccessedField.Local
        private EngineFuel _engineFuel;
        private EngineOdometer _engineOdometer;

        [Inject]
        private void Init(EngineSpeed engineSpeed, EngineFuel engineFuel, EngineOdometer engineOdometer)
        {
            _engineFuel = engineFuel;
            _engineSpeed = engineSpeed;
            _engineOdometer = engineOdometer;
        }

        void Start()
        {
            /*
            VehicleIntegrityCheck();
            GetComponent<PlatformPlacement>().PlaceObjects();
            GetComponent<ModulePlacement>().PlaceObjects();
            */
        }
        
        private void VehicleIntegrityCheck()
        {
            if (_engineSpeed == null)
            {
                Debug.LogError("EngineSpeed is null");
            }
            if (_engineFuel == null)
            {
                Debug.LogError("EngineFuel is null");
            }
            if (_engineOdometer == null)
            {
                Debug.LogError("EngineOdometer is null");
            }
            
            
        }
        
        public void Update() {
            _engineOdometer.Update(Time.deltaTime);
            _engineSpeed.Update(Time.deltaTime);
        }
        
    }
}