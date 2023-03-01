using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace ZeroPrep.MineBuddies
{
    public class CarriageHealthBar : MoreMountains.Tools.MMHealthBar
    {
        MoreMountains.Tools.MMProgressBar _healthBar;
        [SerializeField]
        public Transform healthBarPosition;
        void Start()
        {
             
            _healthBar = GetComponentInChildren<MMProgressBar>();
            _healthBar.transform.position = healthBarPosition.position;
        }


    }
}