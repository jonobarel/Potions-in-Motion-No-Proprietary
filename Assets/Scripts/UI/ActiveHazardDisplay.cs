using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.baltamstudios.minebuddies
{
    public class ActiveHazardDisplay : MonoBehaviour
    {
        //This class displays the ActiveHazard in the UI
        Hazard activeHazardObj;

        [SerializeField]
        Image iconDisplay;

        [SerializeField]
        Slider countDownDisplay;
        [SerializeField]
        Slider fixProgressDisplay;

        
        GameObject hazard; //TODO: this should become a Hazard object 

        void Start()
        {
            if (activeHazardObj == null)
            {
                Destroy(gameObject, 1f);
                throw new System.ArgumentNullException($"{name}: no Hazard object defined");
            }
            Sprite s = HazardManager.Instance.GetIconForHazardType(activeHazardObj.type);
        }

        // Update is called once per frame
        void Update()
        {
            countDownDisplay.value = activeHazardObj.TimeRemaining / activeHazardObj.InitialDuration;
            fixProgressDisplay.value = activeHazardObj.FixProgress;
        }
    }
}