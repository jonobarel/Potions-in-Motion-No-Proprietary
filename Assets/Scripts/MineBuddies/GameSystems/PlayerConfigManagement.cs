using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class PlayerConfigManagement : MonoBehaviour
    {
        public enum PlayState
        {
            PLAYERJOIN,
            GAME
        };

        public PlayState CurrentState { get; private set; }
        
        
        private List<PlayerConfig> _PlayerConfigsList;

        private List<Sprite> _availableSkins;
        [SerializeField]
        private GameObject _playerJoinContainer;
        public GameObject PlayerJoinContainer => _playerJoinContainer;

        private bool _allPlayersReady = false;

        private GameObject _allPlayersReadyUI;

        public UnityEvent<bool> OnPlayersReady;

        void Awake()
        {
            _PlayerConfigsList = new List<PlayerConfig>();

            DontDestroyOnLoad(gameObject);

        }

        [Inject]
        void Init([Inject(Id = "PlayerJoinContainer")] GameObject playerJoinContainer, [Inject(Id = "AllPlayersReadyUI")]GameObject allPlayersReady, PlayState currentState)
        {
            CurrentState = currentState;
            _playerJoinContainer = playerJoinContainer;
            _allPlayersReadyUI = allPlayersReady;
        }

        public void HandlePlayerJoin(PlayerInput pi) 
        {
            Debug.Log("Player joined " + pi.playerIndex);
            pi.transform.SetParent(transform);

            if (!_PlayerConfigsList.Any(p => p.PlayerIndex == pi.playerIndex))
            {
                _PlayerConfigsList.Add(new PlayerConfig(pi));
                AllPlayersReady((_PlayerConfigsList.All(p => p.isReady)));
            }
        }
        
        public void HandlePlayerLeave(PlayerInput pi)
        {
            Debug.Log($"Player Left: {pi.playerIndex}");
            /*PlayerConfig p = _PlayerConfigsList.Find(p => p.PlayerIndex == pi.playerIndex);
            if (p != null)
            {
                _PlayerConfigsList.Remove(p);
               
            }*/
            AllPlayersReady((_PlayerConfigsList.All(p => p.isReady)));
        }

        public void ReadyPlayer(int index, bool state = true)
        {
            _PlayerConfigsList[index].isReady = state;

            AllPlayersReady((_PlayerConfigsList.All(p => p.isReady)));
        }

        private void AllPlayersReady(bool ready)
        {
            if (_allPlayersReady ^ ready) //change in ready status
            {
                _allPlayersReadyUI.SetActive(ready);
                _allPlayersReady = ready;
                if (OnPlayersReady != null)
                {
                    OnPlayersReady.Invoke(ready);
                }
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