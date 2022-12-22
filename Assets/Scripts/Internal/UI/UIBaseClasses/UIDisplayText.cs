using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UIDisplayText<TDisplayable> : UIDisplayBase<TDisplayable, TextMeshProUGUI>
        where TDisplayable : IDisplayable
    {
        protected TextMeshProUGUI _textMesh;

        public override void Awake()
        {
            base.Awake();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }
    }
}