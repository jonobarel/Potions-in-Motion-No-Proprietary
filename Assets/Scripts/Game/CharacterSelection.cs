using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.SceneManagement;

namespace com.baltamstudios.minebuddies
{
    public class CharacterSelection : MonoBehaviour
    {
        static CharacterSelection instance = null;

        public GameObject[] PlayerPrefabs;
        public Transform[] SpawnPoints;
        public bool[] Participating;
        public Dictionary<string, int> Participants;


        public static CharacterSelection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<CharacterSelection>();
                }
                return instance;
            }
        }

        private void Start()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Participating = new bool[PlayerPrefabs.Length];
            Participants = new Dictionary<string, int>();
        }
        public void Update()
        {
            if (Input.GetButtonDown("Player1_Jump"))
            {
                InstantiatePlayer(0);
            }
            if (Input.GetButtonDown("Player2_Jump"))
            {
                InstantiatePlayer(1);
            }
            if (Input.GetButtonDown("Player3_Jump"))
            {
                InstantiatePlayer(2);
            }
            if (Input.GetButtonDown("Player4_Jump"))
            {
                InstantiatePlayer(3);
            }

            if (Input.GetButtonDown("Player1_Pause") || 
                Input.GetButtonDown("Player2_Pause") ||
                Input.GetButtonDown("Player3_Pause") ||
                Input.GetButtonDown("Player4_Pause") )
            {
                if (Participants.Count > 0 && SceneManager.GetActiveScene().name.Equals("MineBuddiesCharacterJoin"))
                    MoreMountains.Tools.MMSceneLoadingManager.LoadScene("CorgiCarriage");
            }


        }

        void InstantiatePlayer(int i)
        {
            if (!Participants.ContainsKey(PlayerPrefabs[i].GetComponent<Character>().PlayerID))
            {
                GameObject player = Instantiate(PlayerPrefabs[i], null);
                player.transform.position = SpawnPoints[i].position;
                player.name = player.GetComponent<Character>().PlayerID;
                //player.GetComponent<MoreMountains.CorgiEngine.Character>().SetPlayerID($"Player{i+1}");
                //player.GetComponent<MoreMountains.CorgiEngine.InputManager>().enabled = false;
                Participants.Add(player.GetComponent<Character>().PlayerID, 0);
            }

            
            
        }

    }
}