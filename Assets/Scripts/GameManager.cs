using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {

        public class Hazard
        {
            public List<HazardType> types;
            public float fixProgress;
            public float countdown;
        }

        public static GameManager Instance
        {
            get { return Managers.Instance.gameManager; }
        }
        void Start()
        {
            /*if (instance != null)
            {
                DestroyImmediate(gameObject);
            }*/
            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                availableHazards.Add(m.hazardType);
            }
        }

        public List<Hazard> ActiveHazards = new List<Hazard>();
        public enum HazardType
        {
            A, B, C, D
        }

        List<HazardType> availableHazards = new List<HazardType>();


        // Update is called once per frame
        void Update()
        {

        }
    }
}