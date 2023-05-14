using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;
using ZeroPrep.UI;

public class UIOdometer : UIDisplayText<EngineOdometer>
{
    private EngineOdometer _odometer;

    [Inject]
    void Init(EngineOdometer odometer)
    {
        Displayable = odometer;
    }

    protected override void OnValueChange(float newValue)
    {
        DisplayText.text = $"{newValue:0}";
    }
    
}
