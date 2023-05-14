using TMPro;
using UnityEngine;


namespace ZeroPrep.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UIDisplayText<TDisplayable> : UIDisplayBase<TDisplayable, TextMeshProUGUI>
        where TDisplayable : IDisplayable
    {
        protected TextMeshProUGUI DisplayText => uiObject;
        public override void Awake()
        {
            base.Awake();
        }
    }
}