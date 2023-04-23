using System;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
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

        public Managers.HazardType HazardType => _hazardType;
        [SerializeField]
        private Managers.HazardType _hazardType;
        private HazardManagerGO _hazardManager;
        private IModuleActivation[] _moduleActivations;

        public IModuleActivation[] ModuleActivations
        {
            get => _moduleActivations;
        }

        [SerializeField]
        private float fuelConsumption;

        public float TreatmentAmount { get; private set; }

      
        // Start is called before the first frame update

                
        [Inject]
        public void Init(EngineFuel engineFuel, HazardManagerGO hazardManager)
        {
            this._engineFuel = engineFuel;
            fuelConsumption = _gameSettings.FuelConsumption;

            _hazardManager = hazardManager;
            _hazardManager.NextHazardInteraction.AddListener(ToggleHazardInteractions);
            TreatmentAmount = _gameSettings.TreatmentEffect;
            
        }

        private void OnDestroy()
        {
            if (_hazardManager != null) {
                _hazardManager.NextHazardInteraction.RemoveListener(ToggleHazardInteractions);
            }
        }

        void Awake()
        {
            _moduleActivations = GetComponents<IModuleActivation>();
        }
        public void Interact(float amount)
        {
            Debug.Log($"{name} - activated");
            if (_engineFuel.RequestFuel(fuelConsumption * amount))
            {
                _hazardManager.TreatHazardOfType(_hazardType, amount);
                
            }
            else
            {
                throw new NotImplementedException("Insufficient fuel, play feedback");
            }            
            
        }

        private void ToggleHazardInteractions(Managers.HazardType type, HazardManagerGO.InteractionType interactions)
        {
            if (HazardType == type)
            {
                foreach (var activator in _moduleActivations)
                {
                    activator.Activable = (activator.InteractionType == interactions);
                }
            }
        }


    }
}