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
    //private PlayerInput[] playersFromCharacterSelection = null;

    [Inject]
    void Init(PlayerInput[] players = null)
    {
        if (players != null)
        {
            
            PlayerPrefabs = (from p in players select p.GetComponent <MineBuddiesInput>().GetPlayerPrefab()).ToArray(); 
        }
        
    }
}
