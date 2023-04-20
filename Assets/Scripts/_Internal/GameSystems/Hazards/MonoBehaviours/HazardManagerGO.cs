using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// The <c>HazardSpawner</c> is a gameobject responsible for generating Hazards.
    /// It exists in UnityEngine because it needs to be integrated for Time, frames, and pauses (if and when they happen).
    /// </summary>
    public class HazardManagerGO : MonoBehaviour
    {
        public enum InteractionType
        {
            None,
            Button,
            Rotation,
            Multi
        }
        
        private GameSettings _gameSettings;

        public bool TimedSpawning = true;

        [SerializeField]
        private bool _isPaused = false;

        private bool _previousIsPaused = false;

        private HazardManager _hazardManager;
        private HazardSpawner _hazardSpawner;

        private EngineSpeed _engineSpeed;

        private AvailableHazardTypes _availableHazardTypes;

        public UnityEvent<Managers.HazardType, InteractionType> NextHazardInteraction; 
        
        public int ActiveHazardsCount()
        {
            return _hazardManager.Hazards.Count;
        }

        [Inject]
        public void Init(HazardManager hazardManager,
            HazardSpawner hazardSpawner,
            EngineSpeed engineSpeed,
            GameSettings gameSettings,
            AvailableHazardTypes availableHazardTypes)
        {
            _hazardManager = hazardManager;
            _hazardSpawner = hazardSpawner;
            _engineSpeed = engineSpeed;
            _gameSettings = gameSettings;
            _availableHazardTypes = availableHazardTypes;
        }

        public void Awake()
        {
            if (_hazardManager == null)
            {
                throw new NullReferenceException("_hazardManager not initialized in Awake");
            }

            HazardBase.Clear += HandleHazardRemoved;
            HazardBase.Expire += HandleHazardRemoved;
            HazardBase.Spawn += HandleHazardSpawn;

        }
        public void Start()
        {
            if (TimedSpawning)
            {
                _hazardSpawner.StartSpawning(this);    
            }
            
        }


        private void HandleHazardRemoved(HazardBase hazard)
        {
            HazardBase h = GetClosestHazardOfType(hazard.Type);
            if (h != null)
            {
                NextHazardInteraction?.Invoke(h.Type, h.InteractionType);
            }
            else
            {
                NextHazardInteraction?.Invoke(hazard.Type, InteractionType.None);
            }
        }

        private void HandleHazardSpawn(HazardBase h)
        {
            HazardBase closestHazard = GetClosestHazardOfType(h.Type);
            if (closestHazard == null || closestHazard == h)
            {
                NextHazardInteraction?.Invoke(h.Type, h.InteractionType);
            }   
        }
        public void Update()
        {
            if (_isPaused)
            {
                _hazardSpawner.StopSpawning();
            }

            else
            {
                _hazardManager.Update(Time.deltaTime * Mathf.Max(_engineSpeed.CurrentSpeed()*_gameSettings.HazardSpeedToEngineRatio, 0.1f));
                //if should spawn but isn't
                if (TimedSpawning && !_hazardSpawner.IsSpawning())
                {
                    _hazardSpawner.StartSpawning(this);
                }
                //if should not spawn but is still spawning
                else if (!TimedSpawning && _hazardSpawner.IsSpawning())
                {
                    _hazardSpawner.StopSpawning();
                }
            }
        }
        
        public HazardBase GetClosestHazardOfType(Managers.HazardType type)
        {
            return _hazardManager.GetClosestHazardOfType(type);
        }

        public void TreatHazardOfType(Managers.HazardType type, float amount)
        {
            HazardBase affectedHazard = GetClosestHazardOfType(type);
            if (affectedHazard != null)
            {
                Debug.Log($"Treating hazard type {type}");
                affectedHazard.TreatAction(amount);
            }
        }

        private void OnDestroy()
        {
            HazardBase.Expire -= HandleHazardRemoved;
            HazardBase.Expire -= HandleHazardRemoved;
            HazardBase.Spawn -= HandleHazardSpawn;
        }
    }
}