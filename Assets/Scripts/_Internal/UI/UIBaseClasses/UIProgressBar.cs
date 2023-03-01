using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(MMProgressBar))]
    public abstract class UIProgressBar<TDisplayable> : UIDisplayBase<TDisplayable, MMProgressBar>
        where TDisplayable :IDisplayable
    {
        protected MMProgressBar MmProgressBar => uiObject;
    }
}