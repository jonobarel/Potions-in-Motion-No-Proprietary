using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class HazardSliderDisplay : MonoBehaviour
    {
        public Transform iconContainer;
        public Image hazardIcon;
        HazardMono _hazardMono;
        public HazardMono HazardMono { 
            set
            {
                if (value != null)
                {
                    
                    var spr = GameSystem.Instance.hazardManagerMono.GetIconForHazardType(value.type);
                    if (spr != null)
                    {
                        hazardIcon.sprite = spr;
                        name = $"Slider {value.name}";
                        _hazardMono = value;
                    }
                    else Debug.LogError($"{name}: could not set hazard icon");

                }
            }
        }
    }
}