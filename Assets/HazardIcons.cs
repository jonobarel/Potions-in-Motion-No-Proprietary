using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.baltamstudios.minebuddies
{
    public class HazardIcons : MonoBehaviour
    {
        [SerializeField]
        List<Sprite> hazardIcons;
        private Dictionary<GameManager.HazardTypes, Sprite> hazardIconsByType = new Dictionary<GameManager.HazardTypes, Sprite>();

        public void Start()
        {
            MatchIconsToHazardEnums();
        }

        private void MatchIconsToHazardEnums()
        {
            if (hazardIcons.Count < System.Enum.GetValues(typeof(GameManager.HazardTypes)).Length)
            {
                throw new System.ArgumentOutOfRangeException($"{name}: not enough icons for hazard types");
            }
            int i = 0;
            foreach (GameManager.HazardTypes e in System.Enum.GetValues(typeof(GameManager.HazardTypes)))
            {
                Debug.Log($"{e}");
                hazardIconsByType.Add(e, hazardIcons[i++]);
                Debug.Log(hazardIconsByType.ToString());
            }
        }

        public Sprite GetIconForHazardType(GameManager.HazardTypes e)
        {
            return hazardIconsByType[e];
        }
    }
}