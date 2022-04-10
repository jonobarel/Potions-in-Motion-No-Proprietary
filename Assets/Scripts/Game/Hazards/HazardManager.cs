using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    [RequireComponent(typeof(HazardIcons))]
    public class HazardManager : MonoBehaviour
    {
        public static HazardManager Instance { get { return GameSystem.Instance.hazardManager; } }
        private ActiveHazards activeHazards;
        public Dictionary<Hazard, float> hazardPositions = new Dictionary<Hazard, float>();

        public Dictionary<GameManager.HazardType, ActionModule> HazardModuleMap;

        public ActiveHazards ActiveHazards { get { return activeHazards; } }
        ConfigManager configManager;

        [SerializeField, Range(0f, 100f)]
        public float StartingDistance = 20f;


        Stack<GameManager.HazardType> nextHazardGroup = new Stack<GameManager.HazardType>();
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
            
            hazardIcons = GetComponent<HazardIcons>();
            activeHazards = GetComponent<ActiveHazards>();

            #region read configuration
            configManager = FindObjectOfType<ConfigManager>();
            StartingDistance = configManager.config.HazardStartingDistance;
            
            HazardModuleMap = new Dictionary<GameManager.HazardType,ActionModule>();
            foreach (ActionModule m in FindObjectsOfType<ActionModule>())
            {
                HazardModuleMap[m.hazardType] = m;
            }

            #endregion

            CreateNextHazardGroup(15f);
        }

        float HazardDifficultyFactor()
        {
            float x = Carriage.Instance.CarriageMovement.DistanceCovered;
            return diffA*x+Mathf.Exp(diffB*x);
        }
        private void CreateNextHazardGroup(float requestedDelay = 0f)
        {

            //decide when the group will start arriving.
            //size of group (factored by cart progress!)
            //delay factor (actual delay will be randomized after each spawn, factored by cart progress)
            //fill the array with random types.
            //Wait until group starts arriving - start coroutine for spawning hazards.

            float difficultyFactor = HazardDifficultyFactor();

            
            float delay = (requestedDelay > 0f) ? requestedDelay : (1f / difficultyFactor) * Random.Range(MinDelay, MaxDelay);
            
            
            nextHazardSpawn = Carriage.Instance.CarriageMovement.DistanceCovered + delay;
            
            
            int size = Random.Range(1, (int)difficultyFactor);
            Debug.Log($"Next hazard group: {size} hazards starting at {nextHazardSpawn}");
            for (int i = 0; i < size; i++)
            {
                nextHazardGroup.Push(GameManager.Instance.availableHazardTypes[Random.Range(0, GameManager.Instance.availableHazardTypes.Count)]);
            }

        }
        void NextHazard()
        {
            if (nextHazardGroup.Count > 0)
            {
                Debug.Log($"Hazard {nextHazardGroup.Count} at {nextHazardSpawn}");
                SpawnHazard(nextHazardGroup.Pop());
                nextHazardSpawn+=HazardDist + Random.Range(-HazardDistOffset, HazardDistOffset);
            }
            if (nextHazardGroup.Count == 0)
                CreateNextHazardGroup();

        }

        // Update is called once per frame
        void Update()
        {
            foreach (Hazard h in new List<Hazard>(hazardPositions.Keys))
            {
                float distance = hazardPositions[h];
                //move the hazards closer to the carriage
                float newDistance = Mathf.Max(distance - Carriage.Instance.CarriageMovement.CurrentSpeed * Time.deltaTime, 0f);
                if (newDistance <= 0f) // move this Hazard to the active set.
                {
                    activeHazards.Add(h);
                    hazardPositions.Remove(h);
                    h.AnimateReachedZero();
                    h.AnimateBecomesActive();
                }
                else
                {
                    hazardPositions[h] = newDistance;
                    h.distanceSlider.value = newDistance / StartingDistance;
                }
            }

            if (Carriage.Instance.CarriageMovement.DistanceCovered > nextHazardSpawn)
            {
                NextHazard();
            }
        }

        public Sprite GetIconForHazardType(GameManager.HazardType e)
        {
            return hazardIcons.GetIconForHazardType(e);
        }

        public void SpawnHazard()
        {
            SpawnHazard(GameManager.Instance.availableHazardTypes[Random.Range(0, GameManager.Instance.availableHazardTypes.Count)]);
        }

        void SpawnHazard(GameManager.HazardType t, float duration = 0f, float startingDistance = 0f )
        {
            if (duration == 0f) duration = configManager.config.InitialDuration;
            if (startingDistance == 0f) startingDistance = configManager.config.HazardStartingDistance;

            Hazard h = Instantiate(hazardPrefab, transform);
            h.SetDuration(duration);
            h.SetType(t);
            h.name = $"Hazard-{h.type}";
            hazardPositions.Add(h, startingDistance);
        }


    }
}