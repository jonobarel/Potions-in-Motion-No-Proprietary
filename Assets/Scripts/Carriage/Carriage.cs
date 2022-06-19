using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Carriage : MonoBehaviour
    {

        PowerModule engine;
        float currentDamage = 0f;
        CarriageMovement carriageMovement;
        public MoreMountains.Tools.MMProgressBar healthBar;

        MoreMountains.CorgiEngine.Health health;
        public CarriageMovement CarriageMovement { get { return carriageMovement; } }
        
        public GameObject HazardSpawner;
        public Transform HazardActivator;
        public GameObject HazardEffectTriggers;

        public float CurrentDamage { get { return currentDamage; } }
        
        public PowerModule Engine { get { return engine; } }

        private static Carriage instance;
        public static Carriage Instance
        {
            get {  
                if (instance == null) { 
                    instance = FindObjectOfType<Carriage>(); 
                } 
                return instance;
            }
        }

        public bool Stalled { get; internal set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError($"{name}: duplicate instance - destroy object by singleton");
                DestroyImmediate(this.gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"{name}: begin Start()");
     
            if (engine == null)
                engine = FindObjectOfType<PowerModule>();
            Debug.Log($"{name}: engine found");
            carriageMovement = GetComponent<CarriageMovement>();
            Debug.Log($"{name}: carriageMovement: {carriageMovement.name}");
            health = GetComponent<MoreMountains.CorgiEngine.Health>();
            Debug.Log($"{name}: Health component: {health.name}");
            if (health == null) Debug.LogError($"{name}: could not find Corgi Health component in Carriage");
            else
            {
                Debug.Log($"{name}: HealthBar component: {healthBar.name}");
                healthBar.SetBar(health.MaximumHealth, 0, health.MaximumHealth);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateHealthBar()
        {
            healthBar.UpdateBar(health.CurrentHealth, 0, health.MaximumHealth);
        }
      
        public void OnDeath()
        {
            carriageMovement.ToggleBrake();
            GameSystem.Instance.analytics.LogEvent("Carriage", Analytics.LogAction.GameEnd, GameManager.HazardType.A, carriageMovement.DistanceCovered, "Total distance");
        }
    }
}