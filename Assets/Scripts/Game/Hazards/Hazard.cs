using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.baltamstudios.minebuddies
{
    public class Hazard : MonoBehaviour
    {
        public GameManager.HazardType type;
        public bool isActive = false;
        public ActiveHazardUI activeUI;
        public MoreMountains.CorgiEngine.Health MMHealth;

        MoreMountains.CorgiEngine.CharacterHorizontalMovement horizontalMovement;

        public void Update()
        {
           if (activeUI != null)
            {
                activeUI.distanceBar.SetBar(SqrDistanceToCarriage(), 0, GameSystem.Instance.gameManager.HazardMaxDistance* GameSystem.Instance.gameManager.HazardMaxDistance);
            }
            
           /*if (isActive && horizontalMovement != null)
            {
                horizontalMovement.WalkSpeed = Carriage.Instance.CarriageMovement.CurrentSpeed, Helpers.Config.HazardProgressAfterActivation);
            }*/

        }

        public void Start()
        {
            horizontalMovement = GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>();
            MMHealth = GetComponent<MoreMountains.CorgiEngine.Health>();
            SetType(GameSystem.GameManager.availableHazardTypes[Random.Range(0, GameSystem.GameManager.availableHazardTypes.Count)]);
            name = $"Hazard-{type}";
            //Debug.Log($"{name}: type {type}");

        }


        public void SetType(GameManager.HazardType t)
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
            activeUI.healthBar.UpdateBar01(1f-(float)MMHealth.CurrentHealth/MMHealth.MaximumHealth);
        }
        public void Activate()
        {
            isActive = true;
            GetComponent<MoreMountains.CorgiEngine.CharacterHorizontalMovement>().WalkSpeed = Helpers.Config.HazardProgressAfterActivation;
            //Debug.Log($"{name}: activated");
        }

        public void Deactivate()
        {
            activeUI.GetComponent<Animate>().DoFadeAnimation();
            GameObject.Destroy(activeUI.gameObject, 1f);
        }
    }
}