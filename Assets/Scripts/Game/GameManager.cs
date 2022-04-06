using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {
        //public List<Hazard> ActiveHazards { get { return GameSystem.Instance.hazardManager.ActiveHazards; } }
        
        public enum HazardType
        {
            A, B, C, D
        }

        public List<HazardType> availableHazardTypes = new List<HazardType>();
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
                availableHazardTypes.Add(m.hazardType);
            }

            Random.InitState(GetRandomSeed());

            Debug.Log($"{name}: available hazard types in this session - {availableHazardTypes}");
        }
      
        int GetRandomSeed()
        {
            ConfigManager configManager = FindObjectOfType<ConfigManager>();
            if (configManager.config.UseRandomSeed)
                return System.Guid.NewGuid().GetHashCode();
            else if (configManager.config.RandomSeed != 0)
                return configManager.config.RandomSeed;
            else throw new System.ArgumentOutOfRangeException("Game set to use FIXED random seed but no random seed selected in config file");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}