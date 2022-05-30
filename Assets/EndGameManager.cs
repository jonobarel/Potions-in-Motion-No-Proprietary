using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI TopPlayerName, HazardNumber, DistanceCovered, RefuelNumber;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player1_Pause") ||
              Input.GetButtonDown("Player2_Pause") ||
              Input.GetButtonDown("Player3_Pause") ||
              Input.GetButtonDown("Player4_Pause"))
        {
            MoreMountains.Tools.MMSceneLoadingManager.LoadScene("MineBuddiesCharacterJoin");
        }
    }
}
