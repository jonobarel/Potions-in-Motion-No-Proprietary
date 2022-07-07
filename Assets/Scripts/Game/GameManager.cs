using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

using UnityEngine.SceneManagement;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class GameManager : MonoBehaviour
    {

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
            get { if (availableHazardTypes == null)
                    InitAvailableHazards();
            return availableHazardTypes; 
                    }
        }

        private void InitAvailableHazards()
        {
            availableHazardTypes = new List<HazardType>();
            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                availableHazardTypes.Add(m.hazardType);
            }

        }

        List<HazardType> availableHazardTypes;
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

                CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
            }
            
        }

        void Start()
        {
            GameSystem.Instance.analytics.Initialize();

            Debug.Log($"{name}: available hazard types in this session - {availableHazardTypes}");


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
                    hazardActivatorDistance = (Carriage.Instance.HazardActivator.transform.position - Carriage.Instance.transform.position).magnitude;
                }    
                return hazardActivatorDistance;
            } 
        }

        // Update is called once per frame

       
    }
}