using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ZeroPrep.MineBuddies
{
    public class HazardHealthBarTest : MonoBehaviour
    {
        public GameManager.HazardType type;
        public bool isActive = false;
        public ActiveHazardUI activeUI;
        public MoreMountains.Tools.MMProgressBar HealthBar;
        

        public MoreMountains.CorgiEngine.Health MMHealth;

        public void Update()
        {

            HealthBar.SetBar(MMHealth.CurrentHealth, 0, MMHealth.MaximumHealth);
            
        }

        public void Start()
        {
            
            MMHealth = GetComponent<MoreMountains.CorgiEngine.Health>();
            name = $"Hazard-{type}";
            Debug.Log($"{name}: type {type}");

        }


    }
}