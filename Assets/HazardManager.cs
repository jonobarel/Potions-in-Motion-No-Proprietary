using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    [RequireComponent(typeof(HazardIcons))]
    public class HazardManager : MonoBehaviour
    {
        public static HazardManager Instance { get { return Managers.Instance.hazardManager; } }

        HazardIcons hazardIcons;
        // Start is called before the first frame update
        void Start()
        {
            hazardIcons = GetComponent<HazardIcons>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Sprite GetIconForHazardType(GameManager.HazardType e)
        {
            return hazardIcons.GetIconForHazardType(e);
        }
    }
}