using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class WheelRotation : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 100f)]
        float RotationSpeedFactor = 30f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var cm = Carriage.Instance.GetComponent<CarriageMovement>();
            transform.Rotate( new Vector3(0, 0, -RotationSpeedFactor*Time.deltaTime * cm.CurrentSpeed));
        }
    }
}