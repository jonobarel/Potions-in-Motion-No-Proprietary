using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class CameraFollow : MonoBehaviour
    {
        Vector3 cameraOffset;
        Transform carriageTransform;
        // Start is called before the first frame update
        void Start()
        {
            carriageTransform = VehicleDamageHandler.Instance.transform;
            cameraOffset = transform.position - carriageTransform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = carriageTransform.position + cameraOffset;
        }
    }
}