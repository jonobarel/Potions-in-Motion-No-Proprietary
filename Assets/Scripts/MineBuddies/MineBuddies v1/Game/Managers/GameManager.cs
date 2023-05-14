using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// This class contains reference to various non-Monobehavior managers available throughout the game.
    /// It is instantiated in the TitleScreen scene, and should be the only one in the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private AnalyticsManager _analytics;

        //private HazardManagerMono _hazardManager;
        //public HazardManagerMono HazardManager => _hazardManager;

        // Start is called before the first frame update
        void Awake()
        {
            if (_instance)
            {
                Debug.Log("Shouldn't have two GameManager objects!");
                throw new Exception("Failed singleton test for GameManagers");
            }
            else
            {
                _instance = this;
                //_hazardManager = new HazardManager();
                //_analytics = new AnalyticsManager();
                
            }

            
        }
    }
}