using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.baltamstudios.minebuddies
{
    public class HazardIcons : MonoBehaviour
    {
        [SerializeField]
        List<Sprite> hazardIcons;
        private Dictionary<GameManager.HazardType, Sprite> hazardIconsByType = new Dictionary<GameManager.HazardType, Sprite>();

        public void Start()
        {
            MatchIconsToHazardEnums();
        }

        private void MatchIconsToHazardEnums()
        {
            if (hazardIcons.Count < System.Enum.GetValues(typeof(GameManager.HazardType)).Length)
            {
                throw new System.ArgumentOutOfRangeException($"{name}: not enough icons for hazard types");
            }
            int i = 0;
            foreach (GameManager.HazardType e in System.Enum.GetValues(typeof(GameManager.HazardType)))
            {
                //Debug.Log($"Type: {e}, image: {hazardIcons[i].name}");
                hazardIconsByType.Add(e, hazardIcons[i++]);
                
            }
            
        }

        public Sprite GetIconForHazardType(GameManager.HazardType e)
        {
            return hazardIconsByType[e];
        }
    }
}