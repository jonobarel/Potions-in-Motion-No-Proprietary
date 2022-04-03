using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.baltamstudios.minebuddies
{
    public class ActiveHazardUI : MonoBehaviour
    {
        //This class displays the ActiveHazard in the UI
        [SerializeField]
        Hazard activeHazardObj;
        public Hazard ActiveHazardObj { get { return activeHazardObj; }
        set { 
                activeHazardObj = value;
                if (iconSprite == null)
                {
                    iconSprite = FindIconForHazard();
                    iconDisplay.sprite = iconSprite;
                }
            }
        }

        [SerializeField]
        Image iconDisplay;
        Sprite iconSprite;

        [SerializeField]
        Slider countDownDisplay;
        [SerializeField]
        Slider fixProgressDisplay;

        
        GameObject hazard; //TODO: this should become a Hazard object 

        void Start()
        {
            //if (activeHazardObj != null )
            //Sprite s = HazardManager.Instance.GetIconForHazardType(activeHazardObj.type);
        }

        // Update is called once per frame
        void Update()
        {
            if (activeHazardObj != null)

            { 
                countDownDisplay.value = activeHazardObj.TimeRemaining / activeHazardObj.InitialDuration;
                fixProgressDisplay.value = activeHazardObj.FixProgress;
            }
            
            
        }

        Sprite FindIconForHazard()
        {
            Sprite s = null;

            if (activeHazardObj != null)
            {
                s = HazardManager.Instance.GetIconForHazardType(activeHazardObj.type);
                Debug.Log($"{name}: Hazard type: {activeHazardObj.type}, icon name: {s.name}");
            }
                return s;
        }

    }
}