using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.InputSystem;
using Zenject.SpaceFighter;


namespace ZeroPrep.MineBuddies
{


    public class MineBuddiesInput : MonoBehaviour
    {
        private bool _isInputReady = false;
        public InputSystemManagerEventsBased InputSystemManager { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        private PlayerSelect _playerJoinPanel;
        private bool _playerReady;
        private GameObject _playerJoinContainer;
        
        [SerializeField] private GameObject playerJoinUIPrefab;
        private PlayerConfigManagement _playerConfigManagement;

        void Awake()
        {
            InputSystemManager = GetComponent<InputSystemManagerEventsBased>();
            PlayerInput = GetComponent<PlayerInput>();
            SetPlayer(PlayerInput.playerIndex);
            _playerConfigManagement = GetComponentInParent<PlayerConfigManagement>();
        }


        void SetPlayer(int i)
        {
            gameObject.name = $"Player{i}";
            InputSystemManager.PlayerID = $"Player{i}";
        }

        void Start()
        {
            _isInputReady = true;
            Debug.Log($"{name} Start");
        }
        public void SpawnPlayerUI(GameObject joinContainer)
        {
            _playerJoinPanel = GameObject.Instantiate(playerJoinUIPrefab, joinContainer.transform).GetComponent<PlayerSelect>();
        }

        public void OnGo(InputValue value)
        {
            if ( value.isPressed && _isInputReady)
            {
                _playerReady = true;
                _playerJoinPanel.SetReady(true);
                _playerConfigManagement.ReadyPlayer(PlayerInput.playerIndex);
            }
        }

        public void OnBack(InputValue value)
        {
            if (value.isPressed && _isInputReady)
            {
                if (_playerReady)
                {
                    _playerReady = false;
                    _playerJoinPanel.SetReady(false);
                    _playerConfigManagement.ReadyPlayer(PlayerInput.playerIndex, false);    
                }
                else
                {
                    CleanUpAndDestroy();
                }
            }
        }

        private void CleanUpAndDestroy()
        {
            Destroy(_playerJoinPanel.gameObject);
            Destroy(gameObject);
        }

    }
}