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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float currentSpeed = Carriage.Instance.CurrenSpeed;
            transform.Rotate( new Vector3(0, 0, -rotationSpeedFactor*Time.deltaTime * currentSpeed));
        }
    }
}