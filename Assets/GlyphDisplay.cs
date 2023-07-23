using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class GlyphDisplay : MonoBehaviour
    {
        SpriteRenderer _renderer;
        private Module _module;


        public void Start()
        {
            _module.HazardTypeChanged += OnHazardTypeChanged;
            if (_module.HazardType != null)
            {
                _renderer.sprite = _module.HazardType.Glyph;
            }
        }

        public void OnDestroy()
        {
            if (_module != null)
            {
                _module.HazardTypeChanged -= OnHazardTypeChanged;    
            }
            
        }

        private void OnHazardTypeChanged(Module module, HazardType oldType, HazardType newType)
        {
            _renderer.sprite = newType.Glyph;
        }

        [Inject]
        public void Init()
        {
            _module = GetComponentInParent<Module>();
            _renderer = GetComponent<SpriteRenderer>();

            HazardType type = _module.HazardType;
            _renderer.sprite = type.Glyph;
        }
    }
}