using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class MineBuddiesCountdown : MMCountdown
    {
        [SerializeField] TextMeshProUGUI tmp;
        
        protected override void UpdateText()
        {
            base.UpdateText();
            tmp.text = _text.text;
        }
    }
}