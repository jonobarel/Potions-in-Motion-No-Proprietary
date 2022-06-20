using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class CameraFollow : MonoBehaviour
    {
        Vector3 cameraOffset;
        Transform carriageTransform;
        // Start is called before the first frame update
        void Start()
        {
            carriageTransform = Carriage.Instance.transform;
            cameraOffset = transform.position - carriageTransform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = carriageTransform.position + cameraOffset;
        }
    }
}