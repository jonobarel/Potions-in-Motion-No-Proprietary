using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ZeroPrep.MineBuddies
{
    [System.Serializable]
    public class HazardType
    {
        private static Dictionary<int, HazardType> _availableTypes = new Dictionary<int, HazardType>();

        public static HazardType[] AvailableTypes
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                    _initialized = true;
                }
                return _availableTypes.Values.ToArray();
            }
            
        }

        private static HazardType _defaultHazardType;
        public static HazardType DefaultHazardType
        {
            get
            {
                if (_defaultHazardType == null)
                {
                    _defaultHazardType = new HazardType(0, DefaultGlyph);
                }

                return _defaultHazardType;
            }
        }

        private static Sprite _defaultGlyph;
        public static Sprite DefaultGlyph
        {
            get
            {
                if (_defaultGlyph == null)
                {
                    _defaultGlyph = Addressables.LoadAssetAsync<Sprite>(_glyphAddresses[0]).WaitForCompletion();
                }

                return _defaultGlyph;
            }
        }

        public static string[] GlyphAddresses => _glyphAddresses;
        
        private static string[] _glyphAddresses = new []
        {
            "glyph_1",
            "glyph_2",
            "glyph_3",
            "glyph_4",
            "glyph_5",
            "glyph_6",
            "glyph_7",
            "glyph_8",
            "glyph_9"
        };


        private static bool _initialized = false;

        [SerializeField] private int typeID;
        public int TypeID { get => typeID; private set => typeID = value; }
        
        [SerializeField] private Sprite glyph;
        public Sprite Glyph { get => glyph ; private set => glyph = value; }
        
        private HazardType(int typeID, Sprite glyph)
        {
            TypeID = typeID;
            Glyph = glyph;
        }
        
        private HazardType(int typeID, string glyphAddress)
        {
            TypeID = typeID;
            Glyph = Addressables.LoadAssetAsync<Sprite>(glyphAddress).WaitForCompletion();
        }

        private static void Initialize()
        {
            for (int i = 0; i < _glyphAddresses.Length; i++)
            {
                HazardType h = new HazardType(i, _glyphAddresses[i]);
                _availableTypes.Add(i, h);
            }
        }

        public static HazardType GetRandomHazardType()
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }
            
            HazardType[] types = AvailableTypes;
            return types[Random.Range(0, types.Length)];

        }

        public static HazardType GetHazardType(int i)
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }
            return _availableTypes.TryGetValue(i, out var type) ? type : null;
        }
    }
}