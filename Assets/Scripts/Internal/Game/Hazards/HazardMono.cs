using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ZeroPrep.MineBuddies
{
    public class HazardMono : MonoBehaviour
    {
        public static int index = 0;

        public Managers.HazardType type;
        public bool isActive = false;
        public ActiveHazardUI activeUI;
        public MoreMountains.CorgiEngine.Health MMHealth;

        public int id;

        [SerializeField]
        Slider positionSlider;

        float startingDistanceSqr;

        MoreMountains.CorgiEngine.CharacterHorizontalMovement horizontalMovement;
        MoreMountains.CorgiEngine.DamageOnTouch MMdamageOnTouch;
        

        public void Update()
        {
           if (activeUI != null)
            {
                //activeUI.distanceBar.SetBar(SqrDistanceToCarriage(), 0, GameSystem.Instance.gameManager.HazardMaxDistance* GameSystem.Instance.gameManager.HazardMaxDistance);
            }

           if (positionSlider != null)
            {
                var distanceToActivatorSqr = (transform.position - GameSystem.Instance.hazardManagerMono.hazardActivator.transform.position).sqrMagnitude;
                positionSlider.value = Mathf.Sqrt(distanceToActivatorSqr / startingDistanceSqr);
                if (isActive)
                    GameObject.Destroy(positionSlider.gameObject);
            }

        }

        public void Start()
        {
            id = index++;
            horizontalMovement = GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>();
            MMHealth = GetComponent<MoreMountains.CorgiEngine.Health>();
            MMdamageOnTouch = GetComponent<MoreMountains.CorgiEngine.DamageOnTouch>();
            MMdamageOnTouch.DamageCaused = GameSystem.Instance.configManager.config.HazardDamageRating;

            SetType(GameSystem.Managers.AvailableHazardTypes[Random.Range(0, GameSystem.Managers.AvailableHazardTypes.Count)]);
            name = $"Hazard-{type}";
            //Debug.Log($"{name}: type {type}");
            //Spawn the timeline indicator
            var hazardManager = GameSystem.Instance.hazardManagerMono.GetComponent<HazardManagerMono>();
            FindObjectOfType<AnalyticsManager>().LogEvent("", Analytics.LogAction.HazardSpawn, type, 1, "hazard spawned", id);
            positionSlider = Instantiate(hazardManager.PositionSliderPrefab, hazardManager.HazardDistanceSliderContainer);
            positionSlider.GetComponent<HazardSliderDisplay>().HazardMono = this;
            startingDistanceSqr = (transform.position - GameSystem.Instance.hazardManagerMono.hazardActivator.transform.position).sqrMagnitude;
        }

        internal void ResumeHazardsAdvancement()
        {
            GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>().MovementSpeed = 2;
        }

        internal void PauseHazardsAdvancement()
        {
            GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>().MovementSpeed = 0;
        }

        public void SetType(Managers.HazardType t)
        {
            type = t;
        }

        public float SqrDistanceToCarriage()
        {
            if (isActiveAndEnabled)
            {
                return (transform.position - Carriage.Instance.transform.position).sqrMagnitude;
                
            }
            else return float.MaxValue;
        }

        public void UpdateUIHealth()
        {
            var healthRatio = (float)MMHealth.CurrentHealth / MMHealth.MaximumHealth;
            activeUI.healthBar.UpdateBar01(1f-healthRatio);
            MMdamageOnTouch.DamageCaused = (int)(healthRatio * GameSystem.Instance.configManager.config.HazardDamageRating);


        }
        public void Activate()
        {
            isActive = true;
            GameSystem.Instance.hazardManagerMono.ActiveHazards.Add(this);
            FindObjectOfType<AnalyticsManager>().LogEvent("", Analytics.LogAction.HazardActivate, type, 1, "hazard activated", id);
            GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>().WalkSpeed = Helpers.Config.HazardProgressAfterActivation;
            //Debug.Log($"{name}: activated");
        }

        public void Deactivate()
        {
            isActive = false;
            FindObjectOfType<AnalyticsManager>().LogEvent("", Analytics.LogAction.DestroyHazard, type, 1, "hazard destroyed", id);
            GameSystem.Instance.hazardManagerMono.ActiveHazards.Remove(this);
            activeUI.GetComponent<Animate>().DoFadeAnimation();
            GameObject.Destroy(activeUI.gameObject, 1f);
            
        }
    }
}