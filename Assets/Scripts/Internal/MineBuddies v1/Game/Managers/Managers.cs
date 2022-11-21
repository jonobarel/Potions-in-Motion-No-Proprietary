using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace ZeroPrep.MineBuddies
{
    public class Managers : MonoBehaviour
    {
        
        //TODO: move these to a UI manager
        [SerializeField]
        private TextMeshProUGUI _odometer;
        [SerializeField]
        private TextMeshProUGUI _speedometer;

        public TextMeshProUGUI Odometer => _odometer;
        public TextMeshProUGUI Speedometer => _speedometer;

        float hazardActivatorDistance = -1;

        public Character[]  playerPrefabs;
        
        public int RandomSeed;
        public Transform[] SpawnPoints;

        public int SessionID;
        

        public enum HazardType
        {
            A, B, C, D, E, F
        }
        public List<HazardType> AvailableHazardTypes
        {
            get { if (_availableHazardTypes == null)
                    InitAvailableHazards();
                return _availableHazardTypes; 
                    }
        }

        private void InitAvailableHazards()
        {
            _availableHazardTypes = new List<HazardType>();
            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                _availableHazardTypes.Add(m.hazardType);
            }

        }

        List<HazardType> _availableHazardTypes;
        private void Awake()
        {
            RandomSeed = GetRandomSeed();
            Random.InitState(RandomSeed);

        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CorgiCarriage")
            {
                SessionID = System.DateTime.Now.GetHashCode();
                Debug.Log($"SessionID: {SessionID}");

                CharacterSelection players = CharacterSelection.Instance;
                if (players == null)
                {
                    Debug.LogError("Error finding participating players");
                    //InstantiateDefaultPlayers();
                }

                FindObjectOfType<AnalyticsManager>().LogEvent("Game", Analytics.LogAction.GameStart, HazardType.A, 0, "Seed id=", RandomSeed);

            }
            
        }

        void Start()
        {
            GameSystem.Instance.analytics.Initialize();

            Debug.Log($"{name}: available hazard types in this session - {_availableHazardTypes}");


        }
      
        int GetRandomSeed()
        {
            ConfigManager configManager = FindObjectOfType<ConfigManager>();
            if (configManager.config.UseRandomSeed)
                return System.Guid.NewGuid().GetHashCode();
            else if (configManager.config.RandomSeed != 0)
                return configManager.config.RandomSeed;
            else throw new System.ArgumentOutOfRangeException("Game set to use FIXED random seed but no random seed selected in config file");
        }

        public float HazardMaxDistance { 
            get { 
                if (hazardActivatorDistance < 0)
                {
                    hazardActivatorDistance = (VehicleDamageHandler.Instance.hazardActivator.transform.position - VehicleDamageHandler.Instance.transform.position).magnitude;
                }    
                return hazardActivatorDistance;
            } 
        }

        // Update is called once per frame

       
    }
}