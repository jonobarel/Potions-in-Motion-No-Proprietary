using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;

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
        _textMesh.text = $"{newValue:0}";
    }
    
}
