using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {
        public List<Hazard> ActiveHazards { get { return GameSystem.Instance.hazardManager.ActiveHazards; } }
        
        public enum HazardType
        {
            A, B, C, D
        }

        public List<HazardType> availableHazards = new List<HazardType>();
        public static GameManager Instance
        {
            get { return GameSystem.Instance.gameManager; }
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

            Random.InitState(GetRandomSeed());

            Debug.Log($"{name}: available hazard types in this session - {availableHazards}");
        }
      
        int GetRandomSeed()
        {
            return (int)(1000*Time.time);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}