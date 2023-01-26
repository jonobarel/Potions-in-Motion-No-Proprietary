using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.InputSystem;


namespace ZeroPrep.MineBuddies
{


    public class MineBuddiesInput : MonoBehaviour
    {
        private bool _isInputReady = false;
        public InputSystemManagerEventsBased InputSystemManager { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        private PlayerSelect _playerJoinPanel;
        
        private GameObject _playerJoinContainer;
        
        [SerializeField] private GameObject playerJoinUIPrefab;


        void Awake()
        {
            InputSystemManager = GetComponent<InputSystemManagerEventsBased>();
            PlayerInput = GetComponent<PlayerInput>();
            SetPlayer(PlayerInput.playerIndex);
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

                _playerJoinPanel.SetReady(true);
                transform.parent.GetComponent<PlayerConfigManagement>().ReadyPlayer(PlayerInput.playerIndex);
            }
        }

    }
}