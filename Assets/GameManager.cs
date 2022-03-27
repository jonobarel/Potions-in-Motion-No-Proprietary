using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {
        class Hazard
        {
            public List<HazardTypes> types;
            public float fixProgress;
            public float countdown;
        }

        public enum HazardTypes
        {
            A, B, C, D
        }

        List<HazardTypes> availableHazards = new List<HazardTypes>();

        // Start is called before the first frame update
        void Start()
        {
            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                availableHazards.Add(m.hazard);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}