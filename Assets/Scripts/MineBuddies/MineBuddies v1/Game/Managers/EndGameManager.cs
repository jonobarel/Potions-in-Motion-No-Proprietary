using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.CorgiEngine;

namespace ZeroPrep.MineBuddies
{
    public class EndGameManager : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI TopPlayerName, HazardNumber, DistanceCovered, RefuelNumber;


        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Player1_Pause") ||
                  Input.GetButtonDown("Player2_Pause") ||
                  Input.GetButtonDown("Player3_Pause") ||
                  Input.GetButtonDown("Player4_Pause"))
            {
                MoreMountains.Tools.MMSceneLoadingManager.LoadScene("MineBuddiesTitleScreen");
            }
        }

       IEnumerator LoadTitleScreen()
        {
            yield return new WaitForSeconds(30f);
            MoreMountains.Tools.MMSceneLoadingManager.LoadScene("MineBuddiesTitleScreen");
        }
    }
}