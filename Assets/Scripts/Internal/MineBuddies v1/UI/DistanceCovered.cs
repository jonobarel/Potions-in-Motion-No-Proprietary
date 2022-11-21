using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZeroPrep.MineBuddies
{
    public class DistanceCovered : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI bodyText;
        
        // Start is called before the first frame update
        void Start()
        {
            bodyText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            var dist = (int)(VehicleDamageHandler.Instance.GetComponent<CarriageMovement>().DistanceCovered);
            //if (dist % 10 == 0) Debug.Log($"Distance covered: {dist}");
            bodyText.text = $"{dist}m";
        }
    }
}