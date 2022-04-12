using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    public class PlayerManager : MonoBehaviour
    {
        //public static PlayerManager Instance { get { return GameSystem.Instance.playerManager; } }

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
            p.GetComponent<Dwarf>().SetColor(playerColors[p.playerIndex]);
            if (playerStartPositions.Length < 1)
                throw new System.ArgumentOutOfRangeException("Not enough player start positions!");
            p.name = $"Player {p.playerIndex+1}";
            Debug.Log($"{p.name} instantiated at {p.transform.position}");
            p.gameObject.GetComponent<Rigidbody2D>().MovePosition(playerStartPositions[p.playerIndex].transform.position);
            Physics.SyncTransforms();

        }
    }
}