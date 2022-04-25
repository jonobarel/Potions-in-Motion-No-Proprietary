using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class GameManager : MonoBehaviour
    {

        
        public int RandomSeed;

        public enum HazardType
        {
            A, B, C, D, E, F
        }

        public List<HazardType> availableHazardTypes = new List<HazardType>();
        private void Awake()
        {
            RandomSeed = GetRandomSeed();
            Random.InitState(RandomSeed);
        }
        void Start()
        {

            ActionModule[] modules = FindObjectsOfType<ActionModule>();
            foreach (var m in modules)
            {
                availableHazardTypes.Add(m.hazardType);
            }

            

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