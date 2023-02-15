using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ZeroPrep.MineBuddies;

public class MineBuddiesMultiplayerLevelManager : MoreMountains.CorgiEngine.MultiplayerLevelManager
{
    private PlayerConfigManagement _playerConfig;
    
    [Inject]
    void Init(PlayerInput[] players, PlayerConfigManagement playerConfigManagement)
    {
        _playerConfig = playerConfigManagement;
        
        //arriving with playerInput objects from another scene:
        if (players is { Length: > 0 })
        {
            //instantiate the relevant players
            PlayerPrefabs = (from p in players select p.GetComponent<MineBuddiesInput>().GetPlayerPrefab()).ToArray();
            return;
        }
    }


    
    
}
