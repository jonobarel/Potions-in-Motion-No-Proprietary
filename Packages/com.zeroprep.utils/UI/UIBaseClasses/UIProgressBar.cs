using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;
using Zenject;

namespace ZeroPrep.UI
{
    [RequireComponent(typeof(MMProgressBar))]
    public abstract class UIProgressBar<TDisplayable> : UIDisplayBase<TDisplayable, MMProgressBar>
        where TDisplayable :IDisplayable
    {
        protected MMProgressBar MmProgressBar => uiObject;
    }
}