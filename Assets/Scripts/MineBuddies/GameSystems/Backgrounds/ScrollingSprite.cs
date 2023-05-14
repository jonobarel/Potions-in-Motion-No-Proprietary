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
        private ScrollingBackground _scrollingBackground;

        public bool FitToCameraOnStart = true;
        
        private ScrollingBackground.BackgroundPair _backgroundPair;
        public ScrollingBackground.BackgroundPair  BackgroundPair
        {
            get => _backgroundPair;
            set
            {
                _backgroundPair = value;
                _parallax_depth = _backgroundPair.Depth;
                _sprite = _backgroundPair.Sprite;
                SpriteRenderer.sprite = _sprite;
                gameObject.name = _backgroundPair.Sprite.name;
            }
        }
        
        private string _current_texture_name = "";
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _parallax_depth = 50;
        
        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }

                return _spriteRenderer;
            }    
        }
        
        void FitCameraHeight() {

            // Get stuff
            double height = SpriteRenderer.sprite.bounds.size.y;
            double worldScreenHeight = Camera.main.orthographicSize * 2.0;

            // Resize
            transform.localScale = new Vector2 (1, 1) * (float)(worldScreenHeight / height);
        }
        
        
        
        public void Init(ScrollingBackground scrollingBackground, ScrollingBackground.BackgroundPair pair)
        {
            _scrollingBackground = scrollingBackground;
            BackgroundPair = pair;

            SetSpriteRendererLayer();
        }

        void Start()
        {
            if (FitToCameraOnStart)
            {
                FitCameraHeight();
            }

            AlignSpriteToLeft();

        }

        private void AlignSpriteToLeft()
        {
            float width = SpriteRenderer.sprite.bounds.size.x;
            float x_pos = Camera.main.orthographicSize * Camera.main.aspect;
            transform.position -= new Vector3(x_pos, 0, 0);
        }
        private void SetSpriteRendererLayer()
        {

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            // want to keep this readable
            if (_backgroundPair.Depth <= _scrollingBackground.GameplayDepth)
            {
                SpriteRenderer.sortingLayerName = _scrollingBackground.BackgroundLayer;
            }
            else
            {
                SpriteRenderer.sortingLayerName = _scrollingBackground.ForegroundLayer;
            }

            SpriteRenderer.sortingOrder = _backgroundPair.Depth;
        }

        
        private void OnSpeedFactorChange(float newValue)
        {
            _speedFactor = newValue;
        }

        
        public void Awake()
        {
            if (_scrollingBackground is not null)
            {
                _scrollingBackground.SpeedChanged += OnSpeedFactorChange;    
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
            _spriteRenderer.sprite = _sprite;
        }

        public void OnDestroy()
        {
            if (_scrollingBackground is not null)
            {
                _scrollingBackground.SpeedChanged -= OnSpeedFactorChange;    
            }
            
        }

        public void LateUpdate()
        {
            Vector2 offset = _material.GetTextureOffset("_MainTex");
            offset.Set(offset.x+Time.deltaTime*_parallax_depth/_speedFactor, offset.y);
            _material.mainTextureOffset = offset;
        }
    }
}