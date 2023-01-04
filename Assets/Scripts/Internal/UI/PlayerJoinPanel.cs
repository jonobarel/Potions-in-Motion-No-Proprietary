using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
using ZeroPrep.MineBuddies;

namespace ZeroPrep.MineBuddies
{


    public class PlayerJoinPanel : MonoBehaviour
    {
        [SerializeField] private int playerID;
        private PlayerJoinManagement _playerJoinManagement;
        private PlayerSkinsList _playerSkins;
        [SerializeField] private JoinPanelUI joinPanelUI; 
        
        void Awake()
        {
            
            joinPanelUI.gameObject.SetActive(false);
        }

        [Inject]
        void Init(PlayerJoinManagement playerJoinManagement, PlayerSkinsList playerSkins)
        {
            _playerJoinManagement = playerJoinManagement;
            _playerSkins = playerSkins;
        }

        void Update()
        {
        }

    }
}