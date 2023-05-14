using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;
using ZeroPrep.UI;

public class UISpeedometer : UIDisplayText<EngineSpeed>
{
    [Inject]
    void Init(EngineSpeed engineSpeed)
    {
        Displayable = engineSpeed;
    }

    protected override void OnValueChange(float newValue)
    {
        DisplayText.text = $"{newValue:0} m/s";
    }
}
