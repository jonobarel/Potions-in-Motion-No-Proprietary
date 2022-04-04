using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameSystem : MonoBehaviour
    {
        public GameManager gameManager;
        public HazardManager hazardManager;
        public PlayerManager playerManager;
        public ConfigManager configManager;

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
        private void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            gameManager = GetComponentInChildren<GameManager>();
            hazardManager = GetComponentInChildren<HazardManager>();
            playerManager = GetComponentInChildren<PlayerManager>();
            configManager = GetComponentInChildren<ConfigManager>();
        }
    }
}