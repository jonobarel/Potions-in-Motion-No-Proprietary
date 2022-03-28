using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Managers : MonoBehaviour
    {
        public GameManager gameManager;
        public HazardManager hazardManager;
        public PlayerManager playerManager;
        

        static Managers instance;
        public static Managers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Managers>();
                }
                return instance;
            }
        }

        private void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            gameManager = GetComponentInChildren<GameManager>();
            hazardManager = GetComponentInChildren<HazardManager>();
            playerManager = GetComponentInChildren<PlayerManager>();
        }
    }
}