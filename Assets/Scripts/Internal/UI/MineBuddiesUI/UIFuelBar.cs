using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
            MmProgressBar.SetBar01(newValue);
        }
    }
}