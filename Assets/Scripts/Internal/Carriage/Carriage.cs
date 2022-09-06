using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZeroPrep.MineBuddies
{
    public class Carriage : MonoBehaviour
    {

        PowerModule _engine;
        float _currentDamage = 0f;

        public float CurrenSpeed => _carriageMovement.CurrentSpeed;
        
        CarriageMovement _carriageMovement;
        public MoreMountains.Tools.MMProgressBar healthBar;

        MoreMountains.CorgiEngine.Health _health;
        public CarriageMovement CarriageMovement { get { return _carriageMovement; } }
        
        [FormerlySerializedAs("HazardSpawner")] public GameObject hazardSpawner;
        [FormerlySerializedAs("HazardActivator")] public Transform hazardActivator;
        [FormerlySerializedAs("HazardEffectTriggers")] public GameObject hazardEffectTriggers;

        public float CurrentDamage { get { return _currentDamage; } }
        
        public PowerModule Engine { get { return _engine; } }

        private static Carriage _instance;
        public static Carriage Instance
        {
            get {  
                if (_instance == null) { 
                    _instance = FindObjectOfType<Carriage>(); 
                } 
                return _instance;
            }
        }

        public bool Stalled { get; internal set; }

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError($"{name}: duplicate instance - destroy object by singleton");
                DestroyImmediate(this.gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"{name}: begin Start()");
     
            if (_engine == null)
                _engine = FindObjectOfType<PowerModule>();
            Debug.Log($"{name}: engine found");
            
            _carriageMovement = new CarriageMovement();
            
            _health = GetComponent<MoreMountains.CorgiEngine.Health>();
            Debug.Log($"{name}: Health component: {_health.name}");
            if (_health == null) Debug.LogError($"{name}: could not find Corgi Health component in Carriage");
            else
            {
                Debug.Log($"{name}: HealthBar component: {healthBar.name}");
                healthBar.SetBar(_health.MaximumHealth, 0, _health.MaximumHealth);
            }
        }

        // Update is called once per frame
        void Update()
        {
            _carriageMovement.Tick(Time.deltaTime);
        }

        public void UpdateHealthBar()
        {
            healthBar.UpdateBar(_health.CurrentHealth, 0, _health.MaximumHealth);
        }
      
        public void OnDeath()
        {
            _carriageMovement.ToggleBrake();
            GameSystem.Instance.analytics.LogEvent("Carriage", Analytics.LogAction.GameEnd, GameManager.HazardType.A, _carriageMovement.DistanceCovered, "Total distance");
        }
    }
}