using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class AnalyticsManager : MonoBehaviour
    {
        Analytics analytics;
        bool ready = false;

        // Update is called once per frame

        public void Initialize()
        {
            if (!ready)
            {
                Debug.Log("AnalyticsManager init");
                analytics = new Analytics(GameSystem.Instance.gameManager.SessionID);
                ready = true;
            }
        }

        public void LogEvent(string playerID, 
            Analytics.LogAction action, 
            GameManager.HazardType hazardType,
            float logValue, string logData, int id = 0)
        {
            if (!ready)
            {
                Initialize();
            }
            analytics.LogEvent(playerID, action, hazardType, logValue, logData, id);
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
            Debug.Log("Dumping analytics to file");
            analytics.CloseLogAndWriteToFile();
            analytics = null;
            ready = false;
        }

        public int GetDistanceCovered()
        { return analytics.GetDistanceCovered(); }

        public string GetTopRefueler()
        {
            return analytics.GetTopRefueler();
        }
    }
}