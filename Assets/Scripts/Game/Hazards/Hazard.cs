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

        public void Update()
        {
           
            
        }

        public void Start()
        {
            
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

        public void Activate()
        {
            Debug.Log($"{name}: activated");
        }
    }
}