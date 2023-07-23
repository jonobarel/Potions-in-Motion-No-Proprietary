using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(PlatformPlacement))]
    public class ModulePlacement : ObjectContainer<Transform>
    {
        [SerializeField] private GameObject modulePrefab;
        [SerializeField] private int moduleCount;

        
        private PlatformPlacement _platformPlacement;

        public PlatformPlacement PlatformPlacement
        {
            get
            {
                if (_platformPlacement == null)
                {
                    _platformPlacement = GetComponent<PlatformPlacement>();
                }

                return _platformPlacement;
            }
            
            set => _platformPlacement = value;
        }

        public override void PlaceObjects()
        {
            base.PlaceObjects(moduleCount, true);

            var platforms = new Stack<Transform>(PlatformPlacement.GetRandomContent(moduleCount));
            var types = new Stack<HazardType> (HazardType.AvailableTypes.MMShuffle().Take(moduleCount));
            
            foreach (var module in Contents)
            {
                module.position = platforms.Pop().position;
                module.GetComponent<Module>().HazardType = types.Pop();
            }

            
        }
        
    }
}