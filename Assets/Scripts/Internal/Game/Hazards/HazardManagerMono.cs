using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(HazardIcons))]
    public class HazardManagerMono : MonoBehaviour
    {
        private ActiveHazards activeHazards;
        public Dictionary<Hazard, float> hazardPositions = new Dictionary<Hazard, float>();

        public Dictionary<Managers.HazardType, ActionModule> HazardModuleMap;

        public TimedSpawner hazardSpawner; //MMCorgiEngine object
        public bool PauseHazards = false;

        public ActiveHazards ActiveHazards { get { return activeHazards; } }
        ConfigManager configManager;

        float StartingDistance = 20f;

        private GameObject hazardEffectTriggers;
        public GameObject HazardEffectTriggers
        {
            get { if (hazardEffectTriggers == null) hazardEffectTriggers = Carriage.Instance.hazardEffectTriggers; return hazardEffectTriggers; }
        }
        
        public Transform HazardDistanceSliderContainer;
        public Slider PositionSliderPrefab;

        public HazardActivator hazardActivator;

        Stack<Managers.HazardType> nextHazardGroup = new Stack<Managers.HazardType>();
        float nextHazardSpawn = 0f;

        float MinDelay { get { return GameSystem.Instance.configManager.config.MinDelay; } }
        float MaxDelay { get { return GameSystem.Instance.configManager.config.MaxDelay; } }
        float diffA { get { return GameSystem.Instance.configManager.config.diffA; } }
        float diffB { get { return GameSystem.Instance.configManager.config.diffB; } }
        float HazardDist { get { return GameSystem.Instance.configManager.config.hazardDist; } }
        float HazardDistOffset { get { return GameSystem.Instance.configManager.config.hazardDistOffset; } }

        Coroutine hazardGeneratorCoroutine;

        [SerializeField]
        Hazard hazardPrefab;
        HazardIcons hazardIcons;
        // Start is called before the first frame update

        void Start()
        {
            // Adjust position of Hazard UI Effect Triggers

           

            hazardIcons = GetComponent<HazardIcons>();
            activeHazards = GetComponent<ActiveHazards>();

            #region read configuration
            configManager = FindObjectOfType<ConfigManager>();
            StartingDistance = configManager.config.HazardStartingDistance;
            
            HazardModuleMap = new Dictionary<Managers.HazardType,ActionModule>();
            foreach (ActionModule m in FindObjectsOfType<ActionModule>())
            {
                HazardModuleMap[m.hazardType] = m;
            }

            #endregion

            Transform[] effectTriggers = HazardEffectTriggers.GetComponentsInChildren<Transform>();
            if (effectTriggers.Length != 3) Application.Quit();
            if (HazardDistanceSliderContainer == null)
            {
                Debug.LogError($"{name}: HazardDistanceSliderContainer not initialized");
            }


            Vector2 carriagePosition = Carriage.Instance.transform.position;
            hazardSpawner.transform.position = carriagePosition + new Vector2(StartingDistance,0);
            hazardActivator.transform.position = carriagePosition + new Vector2(configManager.config.DistanceToActivate, 0);
            
            effectTriggers[1].position = new Vector2(Carriage.Instance.transform.position.x + configManager.config.DistanceToShakeUI1, effectTriggers[1].position.y);
            effectTriggers[2].position = new Vector2(Carriage.Instance.transform.position.x + configManager.config.DistanceToShakeUI2, effectTriggers[2].position.y);

            // CreateNextHazardGroup(15f);
        }


        public Sprite GetIconForHazardType(Managers.HazardType e)
        {
            return hazardIcons.GetIconForHazardType(e);
        }
        /*
        public void SpawnHazard()
        {
            SpawnHazard(GameSystem.GameManager.availableHazardTypes[Random.Range(0, GameSystem.GameManager.availableHazardTypes.Count)]);
        }

        void SpawnHazard(GameManager.HazardType t, float duration = 0f, float startingDistance = 0f )
        {
            if (duration == 0f) duration = configManager.config.InitialDuration;
            if (startingDistance == 0f) startingDistance = configManager.config.HazardStartingDistance;

            Hazard h = Instantiate(hazardPrefab, transform);
            //h.SetDuration(duration);
            h.SetType(t);
            h.name = $"Hazard-{h.type}";
            hazardPositions.Add(h, startingDistance);
        }

        */
    }
}