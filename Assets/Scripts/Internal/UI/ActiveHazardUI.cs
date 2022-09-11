using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace ZeroPrep.MineBuddies
{
    public class ActiveHazardUI : MonoBehaviour
    {
        //This class displays the ActiveHazard in the UI
        [FormerlySerializedAs("activeHazardObj")] [SerializeField]
        HazardMono activeHazardMonoObj;
        public HazardMono ActiveHazardMonoObj { get { return activeHazardMonoObj; }
        set { 
                activeHazardMonoObj = value;
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

        public MoreMountains.Feedbacks.MMFeedback[] feedbacks;

        [SerializeField]
        public MoreMountains.Tools.MMProgressBar distanceBar;
        [SerializeField]
        public MoreMountains.Tools.MMProgressBar healthBar;

        //bool isFading = false;
        GameObject hazard; //TODO: this should become a Hazard object 

        void Start()
        {
            //if (activeHazardObj != null )
            //Sprite s = HazardManager.Instance.GetIconForHazardType(activeHazardObj.type);
            if (healthBar != null)
            {
                healthBar.SetBar01(0);
            }
        }

        // Update is called once per frame

        Sprite FindIconForHazard()
        {
            Sprite s = null;

            if (activeHazardMonoObj != null)
            {
                s = GameSystem.HazardManagerMono.GetIconForHazardType(activeHazardMonoObj.type);
                //Debug.Log($"{name}: Hazard type: {activeHazardObj.type}, icon name: {s.name}");
            }
                return s;
        }

    }
}