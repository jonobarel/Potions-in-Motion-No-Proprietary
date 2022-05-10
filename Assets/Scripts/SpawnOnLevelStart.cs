using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class SpawnOnLevelStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (CharacterSelection.Instance == null)
            {
                Debug.Log("Cannot find characters");
                Application.Quit();
            }
            /*
            bool[] players = CharacterSelection.Instance.PlayerObjs;
            Transform[] SpawnPoints = GetComponentsInChildren<Transform>();
            for (int i= 0; i < players.Length; i++)
            {
                if (players[i]) {
                   
                    
                }
            }
            GameObject.Destroy(CharacterSelection.Instance.gameObject);
            */

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}