using System;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{


    public class Module : MonoBehaviour
    {
        [Inject]
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private GameSettings _gameSettings;
        
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private EngineFuel _engineFuel;
        public EngineFuel EngineFuel => _engineFuel;

        public Managers.HazardType HazardType => _hazardType;
        private Managers.HazardType _hazardType;
        private HazardManagerGO _hazardManager;

        [SerializeField]
        private float fuelConsumption;

        public float TreatmentAmount { get; private set; }
        
        
        
        // Start is called before the first frame update

                
        [Inject]
        public void Init(EngineFuel engineFuel, HazardManagerGO hazardManager)
        {
            if (engineFuel != null)
            {
                this._engineFuel = engineFuel;
            }
            else
            {
                throw new ArgumentException();
            }

            fuelConsumption = _gameSettings.FuelConsumption;

            _hazardManager = hazardManager;
            TreatmentAmount = _gameSettings.TreatmentEffect;

        }
        
        public void Interact(GameObject actor)
        {
            if (!_engineFuel.HasFuel(fuelConsumption))
            {
                throw new NotImplementedException("Insufficient fuel, play feedback");
            }
            else
            {
                _hazardManager.TreatHazardOfType(_hazardType, TreatmentAmount);
            }            
            
        }


    }
}