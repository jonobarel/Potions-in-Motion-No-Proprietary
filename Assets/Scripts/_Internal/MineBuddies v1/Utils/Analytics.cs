using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;

using System;
using System.Linq;
namespace ZeroPrep.MineBuddies
{
    public class Analytics //: MonoBehaviour
    {
        static string FileName = Application.persistentDataPath + "/MineBuddiesLog.csv";

        int sessionID;

        public int SessionID {
            get { return sessionID; }
            set { sessionID = value; }
        }

        public static void InitFile()
        {
            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }
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
            public Managers.HazardType hazardType;
            public float value;
            public string data;
            public int id;

            public string ToCSV()
            {
                return $"{timestamp}, {sessionID}, {playerID}, {action}, {hazardType}, {value}, {data}, {id}";
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
            Managers.HazardType hazardType, float logValue, string logData, int id = 0)
        {
            LogEntry log = new LogEntry();
            log.timestamp = DateTime.Now;
            log.sessionID = SessionID;
            log.playerID = playerID;
            log.action = action;
            log.hazardType = hazardType;
            log.value = logValue;
            log.data = logData;
            log.id = id;

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

        public string GetTopRefueler()
        {
            var refuellingMetrics = (from l in logCollection
                                     where l.action == LogAction.Refuel
                                     group l by l.playerID into refuelMetrics
                                     select new
                                     {
                                         playerID = refuelMetrics.Key,
                                         refuelScore = refuelMetrics.Sum(x => x.value)
                                     });
            string topRefueler = "";

            if (refuellingMetrics.Count() > 0)
            {
                topRefueler = (from l in refuellingMetrics
                               orderby l.refuelScore descending
                               select l.playerID).First();
            }

            return topRefueler;

        }
    }
}