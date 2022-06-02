using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using UnityEngine.SceneManagement;

namespace com.baltamstudios.minebuddies
{
    public class GameSystem : MonoBehaviour
    {
        public GameManager gameManager;
        public HazardManager hazardManager;

        public ConfigManager configManager;
        public AnalyticsManager analytics;

        public static GameManager GameManager { get {return GameSystem.Instance.gameManager;} }
        public static HazardManager HazardManager { get { return GameSystem.Instance.hazardManager;} }
        
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
            }


        }
        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            gameManager = GetComponentInChildren<GameManager>();
            hazardManager = GetComponentInChildren<HazardManager>();
            
            configManager = GetComponentInChildren<ConfigManager>();
        }

        private void Start()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}