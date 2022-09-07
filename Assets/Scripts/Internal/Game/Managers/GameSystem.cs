using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ZeroPrep.MineBuddies
{
    public class GameSystem : MonoBehaviour
    {
        [FormerlySerializedAs("gameManager")] public Managers managers;
        [FormerlySerializedAs("hazardManager")] public HazardManagerMono hazardManagerMono;

        public ConfigManager configManager;
        public AnalyticsManager analytics;

        public static Managers Managers { get {return GameSystem.Instance.managers;} }
        public static HazardManagerMono HazardManagerMono { 
            get { 
                if (Instance.hazardManagerMono == null) 
                    Instance.hazardManagerMono = FindObjectOfType<HazardManagerMono>();
                return GameSystem.Instance.hazardManagerMono;
            } 
        }
        
        public static ConfigManager ConfigManager { get { return GameSystem.Instance.configManager;} }

        static GameSystem instance;
        public static GameSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameSystem>();
                }
                return instance;
            }
        }
        /*
        public MineBuddiesConfigFile Config
        {
            get { return configManager.config; }
        }
        */

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CorgiCarriage")
            {
                Debug.Log($"resetting analytics");
                analytics.Initialize();
                //gameManager.ResetLevel();
                
                foreach (ManagerBase manager in GetComponentsInChildren<ManagerBase>())
                {
                    manager.LevelReset();
                }
            }

            



        }
        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            managers = GetComponentInChildren<Managers>();
            hazardManagerMono = GetComponentInChildren<HazardManagerMono>();
            
            configManager = GetComponentInChildren<ConfigManager>();
        }

        private void Start()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}