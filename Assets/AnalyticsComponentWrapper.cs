using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class AnalyticsComponentWrapper : MonoBehaviour
    {
        Analytics analytics;

        static AnalyticsComponentWrapper instance = null;
        public static AnalyticsComponentWrapper Instance
        {
            get { if (instance == null) instance = FindObjectOfType<AnalyticsComponentWrapper>();
            return instance;}    
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
            analytics = new Analytics(GameSystem.Instance.gameManager.SessionID);
        }

        // Update is called once per frame
        
        public void LogEvent(string playerID, 
            Analytics.LogAction action, 
            GameManager.HazardType hazardType,
            float logValue, string logData)
        {
            analytics.LogEvent(playerID, action, hazardType, logValue, logData);
        }
    }
}