using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.SceneManagement;

namespace ZeroPrep.MineBuddies
{
    public class CharacterSelection : MonoBehaviour
    {
        static CharacterSelection instance = null;

        public GameObject[] PlayerPrefabs;
        public Transform[] SpawnPoints;
        
        public Dictionary<string, GameObject> Participants;


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

        public void Awake()
        {
            if (instance != null)
                GameObject.Destroy(gameObject);
        }

        private void Start()
        {

            GameObject.DontDestroyOnLoad(gameObject);
            Participants = new Dictionary<string, GameObject>();
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        public void Update()
        {
            if (SceneManager.GetActiveScene().name.Equals("MineBuddiesCharacterJoin"))
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


                if (Input.GetButtonDown("StartButton") ||
                    Input.GetKeyDown(KeyCode.Return))
                {
                    if (Participants.Count > 0)
                        MoreMountains.Tools.MMSceneLoadingManager.LoadScene("CorgiCarriage");
                }
            }

            if (Input.GetButtonDown("Cancel")) {
                //check if active players, and drop out accordingly
                if (Participants.Count > 0)
                {
                    if (Input.GetButtonDown("Player1_Cancel"))
                    {
                        RemovePlayer("Player1");
                    }
                    if (Input.GetButtonDown("Player2_Cancel"))
                    {
                        RemovePlayer("Player2");
                    }
                    if (Input.GetButtonDown("Player3_Cancel"))
                    {
                        RemovePlayer("Player3");
                    }
                    if (Input.GetButtonDown("Player4_Cancel"))
                    {
                        RemovePlayer("Player4");
                    }

                }
                else
                {
                    MoreMountains.Tools.MMSceneLoadingManager.LoadScene("MineBuddiesTitleScreen");
                }
                
            }

        }

        void RemovePlayer(string PlayerID) {
            if (Participants.ContainsKey(PlayerID))
            {
                GameObject playerToRemove = Participants[PlayerID];
                Participants.Remove(PlayerID);
                GameObject.Destroy(playerToRemove);
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
                Participants.Add(player.GetComponent<Character>().PlayerID, player);
            }

            
            
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MineBuddiesCharacterJoin")
            {
                Reset();
            }
        }

        public void Reset()
        {
            Participants.Clear();
        }

    }
}