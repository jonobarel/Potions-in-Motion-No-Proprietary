using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] playerStartPositions;

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
            if (playerStartPositions.Length < 1)
                throw new System.ArgumentOutOfRangeException("Not enough player start positions!");
            p.name = $"Player {playerInputManager.playerCount}";
            p.transform.position = playerStartPositions[playerInputManager.playerCount - 1].transform.position;
        }
    }
}