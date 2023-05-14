using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ZeroPrep.MineBuddies
{


    public class PlayerJoinHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPanelHolder;

        void OnPlayerJoined(PlayerInput playerInput)
        {
            playerInput.gameObject.transform.parent = _playerPanelHolder.transform;
        }
    }
}