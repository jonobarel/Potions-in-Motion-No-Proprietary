using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;
using ZeroPrep.UI;

namespace ZeroPrep.MineBuddies
{
    public class UIFuelBar : UIProgressBar<EngineFuel>
    {
        // Start is called before the first frame update

        
        [Inject]
        void Init(EngineFuel engineFuel)
        {
            Displayable = engineFuel;
        }
        
        protected override void OnValueChange(float newValue)
        {
            if (!Mathf.Approximately(newValue, 0f))
            {
                MmProgressBar.SetBar01(newValue);    
            }
            else MmProgressBar.SetBar01(0f);
            
        }
    }
}