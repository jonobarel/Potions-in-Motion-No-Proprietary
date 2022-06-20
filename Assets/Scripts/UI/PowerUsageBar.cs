using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class PowerUsageBar : MonoBehaviour
    {
        [SerializeField]
        Image powerUsageBar;

        void Update()
        {
            powerUsageBar.fillAmount = Carriage.Instance.Engine.PowerDemand;
        }
    }
}