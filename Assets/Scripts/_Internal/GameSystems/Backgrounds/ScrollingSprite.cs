using System;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScrollingSprite : MonoBehaviour
    {
        [SerializeField] private float _baseScrollSpeed = 1f;
        [SerializeField] private float _speedFactor = 1f;
        private Material _material;
        private BackgroundScrolling _backgroundScrolling;

        [Inject]
        private void Init(BackgroundScrolling backgroundScrolling)
        {
            _backgroundScrolling = backgroundScrolling;
        }

        private void OnSpeedFactorChange(float newValue)
        {
            _speedFactor = newValue;
        }

        public void Awake()
        {
            if (_backgroundScrolling is not null)
            {
                _backgroundScrolling.SpeedChanged += OnSpeedFactorChange;    
            }
            
            _material = GetComponent<SpriteRenderer>().material;
        }

        public void OnDestroy()
        {
            if (_backgroundScrolling is not null)
            {
                _backgroundScrolling.SpeedChanged -= OnSpeedFactorChange;    
            }
            
        }

        public void LateUpdate()
        {
            Vector2 offset = _material.GetTextureOffset("_MainTex");
            offset.Set(offset.x+_baseScrollSpeed*Time.deltaTime*_speedFactor, offset.y);
            _material.mainTextureOffset = offset;
        }
    }
}