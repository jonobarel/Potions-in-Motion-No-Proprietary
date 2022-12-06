using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class WheelRotation : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 100f)]
        float rotationSpeedFactor = 30f;

        private EngineSpeed _engineSpeed;

        [Inject]
        private void Init(EngineSpeed engineSpeed)
        {
            _engineSpeed = engineSpeed;
        }
        
        // Update is called once per frame
        void Update()
        {
            float currentSpeed = _engineSpeed.CurrentSpeed();
            transform.Rotate( new Vector3(0, 0, -rotationSpeedFactor*Time.deltaTime * currentSpeed));
        }
    }
}