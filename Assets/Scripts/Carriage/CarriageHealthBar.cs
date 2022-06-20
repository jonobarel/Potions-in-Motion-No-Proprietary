using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class CarriageHealthBar : MoreMountains.Tools.MMHealthBar
    {
        MoreMountains.Tools.MMProgressBar healthBar;
        [SerializeField]
        public Transform healthBarPosition;
        void Start()
        {
             
            healthBar = GetComponentInChildren<MMProgressBar>();
            healthBar.transform.position = healthBarPosition.position;
        }


    }
}