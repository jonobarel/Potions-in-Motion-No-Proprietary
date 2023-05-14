
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace ZeroPrep.MineBuddies
{
    public class ScrollingBackground : MonoBehaviour
    {
        [Serializable]
        public class BackgroundPair
        {
            [SerializeField]
            private int _depth;
            [SerializeField]
            private Sprite _sprite;

            public int Depth
            {
                get
                {
                    if (_sprite != null && _depth == 0)
                    {
                        MatchDepthToSpriteName();
                    }

                    return _depth;
                }
                set => _depth = value;
            }
            public Sprite Sprite
            {
                get => _sprite;
                set
                {
                    _sprite = value;
                    MatchDepthToSpriteName();
                }
            }

            public BackgroundPair(Sprite sprite, int depth)
            {
                this.Sprite = sprite;
                this._depth = depth;
            }

            public void MatchDepthToSpriteName()
            {
                Match match = Regex.Match(Sprite.texture.name, @"bg_(\d+)_");
                _depth = int.Parse(match.Groups[1].Value);
            }
        }
        
        private EngineSpeed _engineSpeed;
        [SerializeField] List<BackgroundPair> _spriteDepthMap = new List<BackgroundPair>();
        [SerializeField] private int _gameplayDepth = 50;
        [SerializeField] private string _actionLayer;
        [SerializeField] private string _backgroundLayer;
        [SerializeField] private string _foregroundLayer;

        public int GameplayDepth => _gameplayDepth;
        public string ActionLayer => _actionLayer;
        public string BackgroundLayer => _backgroundLayer;
        public string ForegroundLayer => _foregroundLayer;
        

        private bool _updateHierarchy = false;
        
        [SerializeField]
        private ScrollingSprite _scrollingSpritePrefab;
        public event Action<float> SpeedChanged;

        
        [Inject]
        void Init(EngineSpeed engineSpeed)
        {
            _engineSpeed = engineSpeed;
        }
        
        void Start()
        {
            if (_engineSpeed is not null)
            {
                _engineSpeed.ValueChanged += OnValueChanged;
            }
        }


        public void OnDestroy()
        {
            if (_engineSpeed != null)
            {
                _engineSpeed.ValueChanged -= OnValueChanged;
            }
        }

        void OnValueChanged(float value)
        {
            if (SpeedChanged != null)
            {
                SpeedChanged.Invoke(value/_engineSpeed.MaxSpeed);
            }
        }

       
        public void RebuildChildren()
        {
            ClearChildren();
            CreateSprites();
        }

        private void ClearChildren()
        {
            ScrollingSprite[] children = GetComponentsInChildren<ScrollingSprite>();
            foreach (var c in children)
            {
                if (Application.isEditor)
                {
                    DestroyImmediate(c.gameObject);
                }
                else 
                {
                    Destroy(c.gameObject);
                }
            }
        }
        
        private void CreateSprites()
        {
            foreach (var pair in _spriteDepthMap)
            {
                ScrollingSprite sprite = Instantiate(_scrollingSpritePrefab, transform);
                
                sprite.Init(this, pair);
            }
        }
        
        
    }
    
   
}