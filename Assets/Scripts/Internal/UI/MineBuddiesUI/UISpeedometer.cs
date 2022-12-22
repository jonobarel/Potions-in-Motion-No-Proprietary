using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;

public class UISpeedometer : UIDisplayText<EngineSpeed>
{
    private EngineOdometer _engineSpeed;

    [Inject]
    void Init(EngineSpeed engineSpeed)
    {
        Displayable = engineSpeed;
    }

    protected override void OnValueChange(float newValue)
    {
        _textMesh.text = $"{newValue:0} m/s";
    }
}
