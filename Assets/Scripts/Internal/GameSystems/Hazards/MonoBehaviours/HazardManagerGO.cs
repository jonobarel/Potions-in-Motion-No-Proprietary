using UnityEngine;
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
        private GameSettings _gameSettings;
        [SerializeField] [Range(0.1f, 20f)] private float minTime = 0.5f;
        [SerializeField] [Range(0.1f, 20f)] private float maxTime = 1.5f;

        public bool TimedSpawning = true;

        private bool _isPaused = false;

        private HazardManager _hazardManager;
        private HazardSpawner _hazardSpawner;
        HazardIcons hazardIcons;

        public Transform hazardDistanceSliderContainer;
        public Slider positionSliderPrefab;

        private EngineSpeed _engineSpeed;

        private AvailableHazardTypes _availableHazardTypes;

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

        public void Start()
        {
            hazardIcons = GetComponent<HazardIcons>();

            //_hazardSpawner = new HazardSpawner(minTime, maxTime, _availableHazardTypes.Types);
            
            if (TimedSpawning)
            {
                _hazardSpawner.StartSpawning(this);    
            }
            
        }

        public void Update()
        {
            
            _hazardManager.Update(Time.deltaTime * Mathf.Min(Carriage.Instance.CurrenSpeed,0.1f));
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
        
        public Sprite GetIconForHazardType(Managers.HazardType e)
        {
            return hazardIcons.GetIconForHazardType(e);
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
                affectedHazard.TreatAction(amount);
            }
        }
    }
}