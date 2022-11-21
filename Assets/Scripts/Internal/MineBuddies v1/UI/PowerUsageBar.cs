using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class PowerUsageBar : MonoBehaviour
    {
        [SerializeField]
        Image powerUsageBar;

        void Update()
        {
            powerUsageBar.fillAmount = VehicleDamageHandler.Instance.Engine.PowerDemand;
        }
    }
}