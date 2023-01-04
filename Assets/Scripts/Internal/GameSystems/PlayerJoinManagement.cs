using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class PlayerJoinManagement : MonoBehaviour
    {
        private List<PlayerJoinPrefs> _playerJoinPrefsList;

        private List<Sprite> _availableSkins;

        void Awake()
        {
            _playerJoinPrefsList = new List<PlayerJoinPrefs>();


        }

        

        void Update()
        {
            if (Input.GetButtonDown("Player1_Jump"))
            {
                JoinPlayer(0);
            }
            if (Input.GetButtonDown("Player2_Jump"))
            {
                JoinPlayer(1);
            }
            if (Input.GetButtonDown("Player3_Jump"))
            {
                JoinPlayer(2);
            }
            if (Input.GetButtonDown("Player4_Jump"))
            {
                JoinPlayer(3);
            }
        }

        private void JoinPlayer(int index)
        {
            Debug.Log($"Player {index} joined");
        }
    }

    class PlayerJoinPrefs
    {
        public Sprite _playerSkin;
        public bool IsReady { get; set; }
        public int PlayerIndex;
        public Color color;
    }
}