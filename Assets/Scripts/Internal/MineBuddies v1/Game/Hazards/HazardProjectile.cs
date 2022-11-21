using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardProjectile : MoreMountains.CorgiEngine.Projectile
    {
        // Start is called before the first frame update
        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (VehicleDamageHandler.Instance != null)
            {
                Speed = VehicleDamageHandler.Instance.CarriageMovement.CurrentSpeed;
            }
        }
    }
}