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

        AnalyticsManager analytics;
        // Start is called before the first frame update
        void Start()
        {
            analytics = FindObjectOfType<AnalyticsManager>();
            if (analytics == null)
                Debug.LogError($"{name}: Could not find analytics object.");
            else
            {
                TopPlayerName.text = analytics.GetTopPlayer();
                HazardNumber.text = $"{analytics.GetTotalHazards()}";
                DistanceCovered.text = $"{analytics.GetDistanceCovered()}";
                RefuelNumber.text = $"{analytics.GetTopRefueler()}";

            }

            if (CharacterSelection.Instance != null)
            {
                CharacterSelection.Instance.Reset();
            }
            

            analytics.DumpToFile();
            
            
            
        }

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