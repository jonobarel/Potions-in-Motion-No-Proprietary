using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;

using System;
using System.Linq;
namespace com.baltamstudios.minebuddies
{
    public class Analytics //: MonoBehaviour
    {
        string FileName = "MineBuddiesLog.csv";

        int sessionID;

        public int SessionID {
            get { return sessionID; }
            set { sessionID = value; }
        }

        public Analytics(int session)
        {
            sessionID = session;
        }

        List<LogEntry> logCollection = new List<LogEntry>();

        struct LogEntry
        {
            public DateTime timestamp;
            public int sessionID;
            public string playerID;
            public LogAction action;
            public GameManager.HazardType hazardType;
            public float value;
            public string data;
        }

        public enum LogAction
        {
            UseModule,
            DamageHazard,
            DestroyHazard,
            DispenseFuel,
            Refuel,
            HazardSpawn,
            HazardActivate,
            HazardDamageCarriage,
            GameStart,
            GameEnd
        };


        public void LogEvent(string playerID, LogAction action, 
            GameManager.HazardType hazardType, float logValue, string logData)
        {
            LogEntry log = new LogEntry();
            log.timestamp = DateTime.Now;
            log.sessionID = SessionID;
            log.playerID = playerID;
            log.action = action;
            log.value = logValue;
            log.data = logData;

            logCollection.Add(log);

        }
        void WriteLogLineToFile(LogEntry log)
        {

        }
        
        public string GetTopPlayer()
        {
            var playerScores = from l in logCollection
                               where l.action == LogAction.DamageHazard
                               group l by l.playerID into playerGroup
                               select new
                               {
                                   playerID = playerGroup.Key,
                                   playerScore = playerGroup.Sum(x => x.value)
                               };

            var topPlayer = (from p in playerScores
                             orderby p.playerScore descending
                             select p.playerID).First();
            return topPlayer;
        }

        public int GetTopPlayerScore()
        {
            var highScore = (from l in logCollection
                             where l.playerID == GetTopPlayer() && l.action == LogAction.DamageHazard
                             select l).Sum(x => x.value);

            return (int)highScore;
                           
        }

        public Dictionary<string, int> GetPlayerScores()
        {
            var scores = (from l in logCollection
                          where l.action == LogAction.DamageHazard
                          group l by l.playerID into playerScores
                          select new
                          {
                              playerID = playerScores.Key,
                              playerScore = playerScores.Sum(x => x.value)
                          });
            Dictionary<string, int> scoreDict = new Dictionary<string, int>();
            foreach (var score in scores)
            {
                scoreDict.Add(score.playerID, (int)score.playerScore);
            }

            return scoreDict;
        }

        public int GetTotalHazards()
        {
            var totalHazards = (from l in logCollection
                                where l.action == LogAction.DestroyHazard
                                select l.value).Sum();

            return (int)totalHazards;
        }
    }
}