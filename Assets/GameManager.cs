using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {
        public class Hazard
        {
            public List<HazardTypes> types;
            public float fixProgress;
            public float countdown;
        }

        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }
                return instance;
            }
        }
        void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                availableHazards.Add(m.hazard);
            }
        }

        public List<Hazard> ActiveHazards = new List<Hazard>();
        public enum HazardTypes
        {
            A, B, C, D
        }

        List<HazardTypes> availableHazards = new List<HazardTypes>();


        // Update is called once per frame
        void Update()
        {

        }
    }
}