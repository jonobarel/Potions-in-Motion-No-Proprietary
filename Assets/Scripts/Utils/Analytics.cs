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
        string FileName = Application.persistentDataPath + "/MineBuddiesLog.csv";

        int sessionID;

        public int SessionID {
            get { return sessionID; }
            set { sessionID = value; }
        }

        public Analytics(int session)
        {
            sessionID = session;
            Debug.Log($"File location: {FileName}");
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

            public string ToCSV()
            {
                return $"{timestamp}, {sessionID}, {playerID}, {action}, {hazardType}, {value}, {data}";
            }
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
            log.hazardType = hazardType;
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

            if (playerScores.Count() > 0)
            {
                var topPlayer = (from p in playerScores
                                 orderby p.playerScore descending
                                 select p.playerID).First();
                return topPlayer;
            }
            else return "None";
            
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

        public void CloseLogAndWriteToFile()
        {
            File.AppendAllLines(FileName, ToCSVArr());

        }

        public String[] ToCSVArr()
        {
            List<string> stringCollection = new List<string>();
            foreach (var log in logCollection)
            {
                stringCollection.Add(log.ToCSV());
            }

            return stringCollection.ToArray();
            
        }

        public int GetDistanceCovered()
        {
            var distanceCovered = (from l in logCollection
                                   where l.sessionID == sessionID && l.action == LogAction.GameEnd
                                   select l.value).Last();
            return (int)distanceCovered;
        }
    }
}