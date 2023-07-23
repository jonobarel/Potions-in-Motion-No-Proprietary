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
        protected EngineFuel _engineFuel;

        public HazardType HazardType
        {
            get => _hazardType;
            set
            {
                HazardType oldType = _hazardType;
                _hazardType = value;
                HazardTypeChanged?.Invoke(this, oldType, _hazardType);
            }
        }

        [SerializeField]
        private HazardType _hazardType;
        private HazardManagerGO _hazardManager;
        
        public event Action<Module, HazardType, HazardType> HazardTypeChanged;
        /// <summary>
        /// Array of available activatable components on the module (e.g. <see cref="MineBuddiesRotationActivated"/>, <see cref="MineBuddiesButtonActivated"/>, etc.)
        /// </summary>
        private IModuleActivation[] _moduleActivations;

        
        /// <summary>
        /// Array of available activatable components on the module (e.g. <see cref="MineBuddiesRotationActivated"/>, <see cref="MineBuddiesButtonActivated"/>, etc.)
        /// </summary>
        public IModuleActivation [] ModuleActivations
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
        public virtual void Interact(float amount)
        {
            if (_engineFuel.RequestFuel(fuelConsumption * amount))
            {
                _hazardManager.TreatHazardOfType(_hazardType, amount);
                
            }
            else
            {
                throw new NotImplementedException("Insufficient fuel, play feedback");
            }            
            
        }

        protected virtual void ToggleHazardInteractions(HazardType type, HazardManagerGO.InteractionType interactions)
        {
            if (HazardType == type)
            {
                foreach (var activator in _moduleActivations)
                {
                    activator.Activable = (activator.InteractionType == interactions);
                }
            }
        }


        protected virtual void OnHazardTypeChanged(Module module, HazardType oldType, HazardType newType)
        {
            HazardTypeChanged?.Invoke(module, oldType, newType);
        }
    }
}