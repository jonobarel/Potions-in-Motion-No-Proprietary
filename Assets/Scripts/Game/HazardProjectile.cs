using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class HazardProjectile : MoreMountains.CorgiEngine.Projectile
    {
        // Start is called before the first frame update
        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (Carriage.Instance != null)
            {
                Speed = Carriage.Instance.CarriageMovement.CurrentSpeed;
            }
        }
    }
}