using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class CameraFollow : MonoBehaviour
    {
        Vector3 cameraOffset;
        [SerializeField]
        Transform carriageTransform;
        // Start is called before the first frame update
        void Start()
        {
            cameraOffset = transform.position - carriageTransform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = carriageTransform.position + cameraOffset;
        }
    }
}