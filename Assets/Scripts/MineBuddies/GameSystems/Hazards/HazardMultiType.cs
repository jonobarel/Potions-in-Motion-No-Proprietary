using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardMultiType : HazardExternal
    {
        public int Segments { get; }

        private Managers.HazardType[] _types;
        public Managers.HazardType[] Types => _types;
        
        private HazardManagerGO.InteractionType[] _interactionTypes;

        public event Action<HazardMultiType> OnFlip;
        public HazardManagerGO.InteractionType[] InteractionTypes
        {
            get
            {
                HazardManagerGO.InteractionType[] interactionTypes =
                    new HazardManagerGO.InteractionType [_interactionTypes.Length];
                _interactionTypes.CopyTo(interactionTypes, 0);
                return interactionTypes;
            }

            set => _interactionTypes = value;
        }

        private float _sectionLength;
        public float SectionLength => _sectionLength;
        
        
        private float _currentSection = 0f;

        private int _index = 0;
        public int Index => _index;
        public int IndexNormalized => _index % _types.Length;
        
        public HazardMultiType(float speed, Managers.HazardType[] types, HazardManagerGO.InteractionType[] interactionTypes, int segments, float startingHealth = 1) 
        : base(speed, types[0], interactionTypes[0], startingHealth)
        {
            if (types.Length != interactionTypes.Length)
            {
                throw new ArgumentException("Types and interactions arrays must be same length");
            }
            this.Segments = segments;
            _sectionLength = Health / segments;
            _types = types;
            _interactionTypes = interactionTypes;
        }

        public override void TreatAction(float treatment)
        {
            _currentSection += treatment;
            if (_currentSection >= _sectionLength)
            {
                NextHazardType();
                _currentSection %= _sectionLength;
            }
            base.TreatAction(treatment);
        }

        private void NextHazardType()
        {
            if (_index < _types.Length - 1)
            {
                _index++;
                Type = _types[IndexNormalized];
                InteractionType = _interactionTypes[IndexNormalized];
                OnFlip?.Invoke(this);    
            }
        }
    }
}