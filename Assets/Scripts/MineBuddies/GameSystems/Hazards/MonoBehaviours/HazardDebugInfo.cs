using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardDebugInfo : MonoBehaviour
    {

        HazardDisplay _hazardDisplay;
        TextMeshProUGUI _text;
        void Awake()
        {
            _hazardDisplay = GetComponentInParent<HazardDisplay>();
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        void Update()
        {
            if (_hazardDisplay != null)
            {
                _text.text = "Type:" + _hazardDisplay.Hazard.Type.ToString();
                _text.text += $"\nID: {_hazardDisplay.Hazard.ID}";
                _text.text += $"\nTreatment: {_hazardDisplay.Hazard.Health*100:00f}";
            }
        }
    }
}