using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

public class MineBuddiesInputSystemManager : InputSystemManagerEventsBased
{

    protected override void BindButton(InputValue inputValue, MMInput.IMButton imButton)
    {
        if (imButton != null)
        {
            base.BindButton(inputValue, imButton);
        }
        
    }

}
