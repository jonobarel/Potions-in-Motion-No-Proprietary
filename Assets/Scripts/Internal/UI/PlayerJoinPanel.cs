using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;
using Zenject.SpaceFighter;
using ZeroPrep.MineBuddies;

namespace ZeroPrep.MineBuddies
{


    public class PlayerJoinPanel : MonoBehaviour
    {
        [SerializeField] private int playerID;
        private PlayerConfigManagement _playerConfigManagement;
        private PlayerSkinsList _playerSkins;
        [SerializeField] private JoinPanelUI joinPanelUI;

        private PlayerInput _playerInput;
        
        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            joinPanelUI.gameObject.SetActive(false);
        }

        void Start()
        {
            
        }
        [Inject]
        void Init(PlayerConfigManagement playerConfigManagement, PlayerSkinsList playerSkins)
        {
            _playerConfigManagement = playerConfigManagement;
            _playerSkins = playerSkins;
        }

        void Update()
        {
        }

        public void OnInteract()
        {
            Debug.Log($"{name} pressed \"interact\"");
        }

    }
}