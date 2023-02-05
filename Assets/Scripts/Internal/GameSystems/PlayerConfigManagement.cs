using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class PlayerConfigManagement : MonoBehaviour
    {
        private List<PlayerConfig> _PlayerConfigsList;

        private List<Sprite> _availableSkins;
        [SerializeField]
        private GameObject _playerJoinContainer;

        private bool _allPlayersReady = false;

        private GameObject _allPlayersReadyUI;
        
        
        void Awake()
        {
            _PlayerConfigsList = new List<PlayerConfig>();

            DontDestroyOnLoad(gameObject);

        }

        [Inject]
        void Init([Inject(Id = "PlayerJoinContainer")] GameObject playerJoinContainer, [Inject(Id = "AllPlayersReadyUI")]GameObject allPlayersReady)
        {
            _playerJoinContainer = playerJoinContainer;
            _allPlayersReadyUI = allPlayersReady;
        }

        public void HandlePlayerJoin(PlayerInput pi) 
        {
            Debug.Log("Player joined " + pi.playerIndex);
            pi.transform.SetParent(transform);
            pi.GetComponent<MineBuddiesInput>().SpawnPlayerUI(_playerJoinContainer);
            
            if (!_PlayerConfigsList.Any(p => p.PlayerIndex == pi.playerIndex))
            {
                _PlayerConfigsList.Add(new PlayerConfig(pi));
                AllPlayersReady((_PlayerConfigsList.All(p => p.isReady)));
            }
        }

        public void ReadyPlayer(int index, bool state = true)
        {
            _PlayerConfigsList[index].isReady = state;

            AllPlayersReady((_PlayerConfigsList.All(p => p.isReady)));
        }

        private void AllPlayersReady(bool ready)
        {
            _allPlayersReadyUI.SetActive(ready);
            _allPlayersReady = ready;
        }

    }
    

    class PlayerConfig
    {
        public Sprite _playerSkin;
        public bool isReady { get; set; }
        public int PlayerIndex { get; private set; }
        public Color _color { get; set; }
        public PlayerInput Input { get; private set; }

        public PlayerConfig(PlayerInput pi)
        {
            Input = pi;
            PlayerIndex = pi.playerIndex;
        }
        
    }
}