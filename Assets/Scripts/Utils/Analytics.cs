using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;


namespace com.baltamstudios
{
    public class Analytics : MonoBehaviour
    {
        // Start is called before the first frame update
        string TimeStamp { get { return System.DateTime.UtcNow.ToString(); } }

        int sessionID;

        //int eventID = 0;

        IDbConnection dbcon;
        void Start()
        {
            string connection = "URI=file:" + Application.persistentDataPath + "/AnalyticsDB.sqlite";
            Debug.Log($"Connection string: {connection}");
            dbcon = new SqliteConnection(connection);
            dbcon.Open();
            InitDatabase();
            sessionID = LogSessionID();
            Debug.Log($"sessionID: {sessionID}");

        }

        private void InitDatabase()
        {
            IDbCommand createSessionsTable;
            createSessionsTable = dbcon.CreateCommand();
            string q_createTable = "CREATE TABLE if not exists \"sessions\" (" +
                "\"SessionID\"	INTEGER UNIQUE,	" +
                "\"StartTime\"	INTEGER,	" +
                "\"RandomSeed\"	INTEGER,	" +
                "PRIMARY KEY(\"SessionID\" AUTOINCREMENT));";
            createSessionsTable.CommandText = q_createTable;
            createSessionsTable.ExecuteReader();

            string q_create_table_events = "CREATE TABLE if not exists \"events\" (    " +
                "\"eventID\"   INTEGER,	" +
                "\"sessionID\" INTEGER NOT NULL,	" +
                "\"TimeStamp\" INTEGER,	" +
                "\"playerID\"  INTEGER,	" +
                "\"eventType\" TEXT,	" +
                "\"module\"    INTEGER,	" +
                "\"hazardType\" INTEGER, " +
                "\"info\"  TEXT," +
                "PRIMARY KEY(\"eventID\" AUTOINCREMENT)); ";

            IDbCommand createEventsTable = dbcon.CreateCommand();
            createEventsTable.CommandText = q_create_table_events;
            createEventsTable.ExecuteReader();
        }

        private int LogSessionID()
        {
            IDbCommand cmnd = dbcon.CreateCommand();
            Debug.Log($"{sessionID}, {System.DateTime.UnixEpoch.Second}");
            cmnd.CommandText = $"INSERT INTO sessions (StartTime, RandomSeed) values (@time, @seed)";
            
            cmnd.Parameters.Add(new SqliteParameter("@time", TimeStamp));
            cmnd.Parameters.Add(new SqliteParameter("@seed", minebuddies.GameSystem.GameManager.RandomSeed));
            
            Debug.Log($"SQLite command: {cmnd.CommandText}, parameters: ");
            int written = cmnd.ExecuteNonQuery();

            cmnd.CommandText = "select max(sessionID) as sessionID from sessions";
            IDataReader reader = cmnd.ExecuteReader();
            //Debug.Log($"Query returned: {reader[0].ToString()}");
            if (reader.Read())
            {
                return int.Parse(reader[0].ToString());
            }
            return -1;

        }
        public void Logger(int playerID = -1, string eventType = "GenericEvent", int moduleID = -1, int hazardType = -1, string info = "")
        {
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = $"insert into events (sessionID, TimeStamp, palyerID, eventType, module, hazardType, info) values (@timestamp, @playerID, @eventtype, @hazardType, @module, @info)";
            cmnd.Parameters.Add(new SqliteParameter("@timestamp", TimeStamp));
            cmnd.Parameters.Add(new SqliteParameter("@playerID", playerID));
            cmnd.Parameters.Add(new SqliteParameter("@eventType", eventType));
            cmnd.Parameters.Add(new SqliteParameter("@hazardType", hazardType));
            cmnd.Parameters.Add(new SqliteParameter("@eventType", hazardType));
            cmnd.Parameters.Add(new SqliteParameter("@info", info));

            if (cmnd.ExecuteNonQuery() != 1)
                Debug.Log($"{name}: logging data - insert returned wrong number of rows");
        }
    }
}