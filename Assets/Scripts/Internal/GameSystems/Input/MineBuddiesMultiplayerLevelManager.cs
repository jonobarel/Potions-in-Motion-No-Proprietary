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
    
    //When game scene starts directly, the characters are spawned by the MMLevelManager. For debugging purposes, this leaves
    //the playerinput objects disconnected from the characters.
    //Adding a process during the Awake to register for player join events.
    public new void Awake()
    {
        _playerConfig.GetComponent<PlayerInputManager>().playerJoinedEvent.AddListener(OnPlayerInputConnected);
    }

    public void OnDestroy()
    {
        if (_playerConfig is not null )
        {
            _playerConfig.GetComponent<PlayerInputManager>().playerJoinedEvent.RemoveListener(OnPlayerInputConnected);
        }
        
    }
    void OnPlayerInputConnected(PlayerInput p)
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (var c in characters)
        {
            c.SetInputManager();
        }
    }



    
    
}
