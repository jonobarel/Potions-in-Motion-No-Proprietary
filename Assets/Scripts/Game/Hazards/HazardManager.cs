using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    [RequireComponent(typeof(HazardIcons))]
    public class HazardManager : MonoBehaviour
    {
        public static HazardManager Instance { get { return GameSystem.Instance.hazardManager; } }
        public List<Hazard> ActiveHazards = new List<Hazard>();
        public Dictionary<Hazard, float> hazardPositions = new Dictionary<Hazard, float>();

        [SerializeField, Range(0f, 100f)]
        public float StartingDistance = 20f;

        [SerializeField]
        Hazard hazardPrefab;
        HazardIcons hazardIcons;
        // Start is called before the first frame update

        void Start()
        {
            hazardIcons = GetComponent<HazardIcons>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Hazard h in new List<Hazard>(hazardPositions.Keys))
            {
                float distance = hazardPositions[h];
                //move the hazards closer to the carriage
                float newDistance = Mathf.Max(distance - Carriage.Instance.CarriageMovement.CurrentSpeed * Time.deltaTime, 0f);
                hazardPositions[h] = newDistance;
                h.distanceSlider.value = newDistance/StartingDistance;
            }
        }

        public Sprite GetIconForHazardType(GameManager.HazardType e)
        {
            return hazardIcons.GetIconForHazardType(e);
        }

        public void SpawnHazard()
        {
            Hazard h = Instantiate(hazardPrefab, transform);
            
            h.SetDuration(10f);
            h.SetType(GameManager.Instance.availableHazards[Random.Range(0, GameManager.Instance.availableHazards.Count)]);
            h.name = $"Hazard-{h.type}";
            hazardPositions.Add(h, StartingDistance);
        }
    }
}