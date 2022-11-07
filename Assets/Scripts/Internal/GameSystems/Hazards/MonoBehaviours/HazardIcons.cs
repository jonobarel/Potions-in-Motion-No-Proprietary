using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZeroPrep.MineBuddies
{
    public class HazardIcons : MonoBehaviour
    {
        [SerializeField]
        List<Sprite> hazardIcons;
        private Dictionary<Managers.HazardType, Sprite> hazardIconsByType = new Dictionary<Managers.HazardType, Sprite>();

        public void Start()
        {
            MatchIconsToHazardEnums();
        }

        private void MatchIconsToHazardEnums()
        {
            if (hazardIcons.Count < System.Enum.GetValues(typeof(Managers.HazardType)).Length)
            {
                throw new System.ArgumentOutOfRangeException($"{name}: not enough icons for hazard types");
            }
            int i = 0;
            foreach (Managers.HazardType e in System.Enum.GetValues(typeof(Managers.HazardType)))
            {
                //Debug.Log($"Type: {e}, image: {hazardIcons[i].name}");
                hazardIconsByType.Add(e, hazardIcons[i++]);
                
            }
            
        }

        public Sprite GetIconForHazardType(Managers.HazardType e)
        {
            return hazardIconsByType[e];
        }
    }
}