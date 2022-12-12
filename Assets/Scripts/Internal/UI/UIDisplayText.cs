using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIDisplayText<TDisplayable> : UIDisplayBase<TDisplayable, TextMeshProUGUI>
        where TDisplayable : IDisplayable
    {
        private TextMeshProUGUI _textMesh;

        public override void Awake()
        {
            base.Awake();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }
        
        protected override void OnValueChange(float nVal)
        {
            _textMesh.text = $"{nVal}";
        }
    }
}