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

            if (activeHazardObj != null)
            {
                s = GameSystem.HazardManager.GetIconForHazardType(activeHazardObj.type);
                Debug.Log($"{name}: Hazard type: {activeHazardObj.type}, icon name: {s.name}");
            }
                return s;
        }

    }
}