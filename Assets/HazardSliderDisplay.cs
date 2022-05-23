using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baltamstudios.minebuddies
{
    public class HazardSliderDisplay : MonoBehaviour
    {
        public Transform iconContainer;
        public Image hazardIcon;
        Hazard hazard;
        public Hazard Hazard { 
            set
            {
                if (value != null)
                {
                    
                    var spr = GameSystem.Instance.hazardManager.GetIconForHazardType(value.type);
                    if (spr != null)
                    {
                        hazardIcon.sprite = spr;
                        name = $"Slider {value.name}";
                        hazard = value;
                    }
                    else Debug.LogError($"{name}: could not set hazard icon");

                }
            }
        }

        


    }
}