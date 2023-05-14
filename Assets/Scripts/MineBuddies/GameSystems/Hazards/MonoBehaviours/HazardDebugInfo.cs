using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardDebugInfo : MonoBehaviour
    {

        HazardSliderDisplay _hazardSliderDisplay;
        TextMeshProUGUI _text;
        void Awake()
        {
            _hazardSliderDisplay = GetComponentInParent<HazardSliderDisplay>();
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        void Update()
        {
            if (_hazardSliderDisplay != null)
            {
                _text.text = "Type:" + _hazardSliderDisplay.Hazard.Type.ToString();
                _text.text += $"\nID: {_hazardSliderDisplay.Hazard.ID}";
                _text.text += $"\nTreatment: {_hazardSliderDisplay.Hazard.Health*100:00f}";
            }
        }
    }
}