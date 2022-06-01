using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
public class ModuleButtonActivated : ButtonActivated
{
    public override void TriggerButtonAction(GameObject instigator)
    {
        base.TriggerButtonAction(instigator);
        Debug.Log($"{instigator.name} pressed my ({name} button!");
    }
}
