using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class CharacterSelection : MonoBehaviour
    {
        static CharacterSelection instance = null;

        public GameObject[] PlayerPrefabs;
        public Transform[] SpawnPoints;
        public GameObject[] PlayerObjs;


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
            PlayerObjs = new GameObject[PlayerPrefabs.Length];
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


        }

        void InstantiatePlayer(int i)
        {
            if (PlayerObjs[i] == null)
            {
                GameObject player = Instantiate(PlayerPrefabs[i]);
                player.transform.position = SpawnPoints[i].position;
                player.name = $"Player{i + 1}";
                player.GetComponent<MoreMountains.CorgiEngine.Character>().SetPlayerID($"Player{i+1}");
                //player.GetComponent<MoreMountains.CorgiEngine.InputManager>().enabled = false;
                PlayerObjs[i] = player;
            }

            
            
        }

    }
}