using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class AnalyticsManager : MonoBehaviour
    {
        Analytics analytics;

        static AnalyticsManager instance = null;
        public static AnalyticsManager Instance
        {
            get { 
                if (instance == null) instance = FindObjectOfType<AnalyticsManager>();
                return instance;
            }    
        }


        void Awake() {
            if (instance != null) {
                GameObject.Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame

        public void Initialize()
        {
            analytics = new Analytics(GameSystem.Instance.gameManager.SessionID);
        }

        public void LogEvent(string playerID, 
            Analytics.LogAction action, 
            GameManager.HazardType hazardType,
            float logValue, string logData)
        {
            analytics.LogEvent(playerID, action, hazardType, logValue, logData);
        }

        public string GetTopPlayer()
        {
            return analytics.GetTopPlayer();
        }

        public int GetTotalHazards()
        {
            return analytics.GetTotalHazards();
        }

        public void DumpToFile()
        {
            analytics.CloseLogAndWriteToFile();
        }

        
    }
}