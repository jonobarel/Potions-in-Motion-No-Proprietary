using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class MineBuddiesCameraController : MoreMountains.CorgiEngine.CameraController
    {
        public Transform MainTarget;
        void Start()
        {
            this.SetTarget(MainTarget);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}