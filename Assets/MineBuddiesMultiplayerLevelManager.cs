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
    private PlayerInput[] _players = null;

    [Inject]
    void Init(PlayerInput[] players = null)
    {
        
        //arriving with playerInput objects from another scene:
        if (players is { Length: > 0 })
        {
            //instantiate the relevant players
            PlayerPrefabs = (Character[])(from p in players select p.GetComponent <MineBuddiesInput>().GetPlayerPrefab());
            return;
        }
    }

    new void Awake()
    {
        base.Awake();
        if (_players is null)
        {
            
        }
    }
    
    
}
