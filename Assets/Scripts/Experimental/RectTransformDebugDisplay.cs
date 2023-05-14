using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class RectTransformDebugDisplay : MonoBehaviour
    {
        
        private RectTransform _rectTransform;

        Vector2 anchorPoints;
        private Vector2 relativeSize;
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
        
        }

        void OnGUI()
        {
            anchorPoints = _rectTransform.anchoredPosition;
            relativeSize = _rectTransform.sizeDelta;
            
            GUI.Label(new Rect(0, 20, 150, 80), $"Anchor Points: {anchorPoints}");
            GUI.Label(new Rect(300, 20, 150, 80), $"Relative size: {relativeSize}");
        }
    }
}