using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    public class PlayerManager : MonoBehaviour
    {
        PlayerInputManager playerInputManager;
        private Color[] playerColors = {Color.red, Color.green, Color.blue, Color.yellow};

        public void Start()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }

        public void OnPlayerJoin(PlayerInput p)
        {
            Debug.Log("Player Joined");
            p.GetComponent<Dwarf>().SetColor(playerColors[playerInputManager.playerCount - 1]);
        }
    }
}