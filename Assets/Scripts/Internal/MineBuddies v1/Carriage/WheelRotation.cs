using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZeroPrep.MineBuddies
{
    public class WheelRotation : MonoBehaviour
    {
        [FormerlySerializedAs("RotationSpeedFactor")]
        [SerializeField]
        [Range(0f, 100f)]
        float rotationSpeedFactor = 30f;
        
        // Update is called once per frame
        void Update()
        {
            float currentSpeed = VehicleDamageHandler.Instance.CurrenSpeed;
            transform.Rotate( new Vector3(0, 0, -rotationSpeedFactor*Time.deltaTime * currentSpeed));
        }
    }
}