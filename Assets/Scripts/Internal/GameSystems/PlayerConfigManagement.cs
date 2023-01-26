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
        
        void Awake()
        {
            _PlayerConfigsList = new List<PlayerConfig>();

            DontDestroyOnLoad(gameObject);

        }

        [Inject]
        void Init([Inject(Id = "PlayerJoinContainer")] GameObject playerJoinContainer)
        {
            _playerJoinContainer = playerJoinContainer;
        }

        public void HandlePlayerJoin(PlayerInput pi) 
        {
            Debug.Log("Player joined " + pi.playerIndex);
            pi.transform.SetParent(transform);
            pi.GetComponent<MineBuddiesInput>().SpawnPlayerUI(_playerJoinContainer);
            
            if (!_PlayerConfigsList.Any(p => p.PlayerIndex == pi.playerIndex))
            {
                _PlayerConfigsList.Add(new PlayerConfig(pi));
            }
        }

        public void ReadyPlayer(int index)
        {
            _PlayerConfigsList[index].isReady = true;

            if (_PlayerConfigsList.All(p => p.isReady))
            {
                Debug.Log("All players ready");
            }
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