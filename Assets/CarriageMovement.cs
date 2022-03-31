using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class CarriageMovement : MonoBehaviour
    {
        float distanceCovered = 0f;
        public float DistanceCovered { get { return distanceCovered; } }
        [SerializeField]
        [Range(0f, 10f)]
        float speedOverride;
        float currentSpeed;
        public float CurrentSpeed { get { return currentSpeed; } }
        float MaxSpeed = 2f / 3.6f; // M/s = 1/3.6* KM/h
        [SerializeField]
        float HazardSlowdownFactor = 0.1f;


        float CalculateSpeed()
        {
            if (speedOverride > 0f)
                return speedOverride;
            //let's set the speed as MaxSpeed - A*activeHazards*(1-damage)
            var speed = MaxSpeed * (1 - Carriage.Instance.CurrentDamage) - HazardSlowdownFactor * GameManager.Instance.ActiveHazards.Count;
            speed = Mathf.Clamp(speed, 0, MaxSpeed);
            return speed;
        }

        void Start()
        {
            speedOverride = 0f;

        }

        // Update is called once per frame
        void Update()
        {
            currentSpeed = CalculateSpeed();
            distanceCovered += currentSpeed * Time.deltaTime;
        }
    }
}