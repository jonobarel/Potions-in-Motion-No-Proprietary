using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class CarriageMovement : MonoBehaviour
    {
        ConfigManager configManager;
        float distanceCovered = 0f;
        public float DistanceCovered { get { return distanceCovered; } }
        [SerializeField]
        [Range(0f, 10f)]
        float speedOverride;
        float currentSpeed;
        public float CurrentSpeed { get { return currentSpeed; } }
        float MaxSpeed { get { return configManager.config.MaxCarriageSpeed; } }
        [SerializeField]
        float HazardSlowdownFactor = 0.1f;

        //for Lerping speed.
        float startSpeed;
        float targetSpeed;
        float speedChangeProgress = 0f;


        float CalculateSpeed()
        {
            if (speedOverride > 0f)
                return speedOverride;
            //let's set the speed as MaxSpeed - A*activeHazards*(1-damage)
            else if (Carriage.Instance.Engine.Fuel > 0)
            {
                var speed = MaxSpeed * (1 - Carriage.Instance.CurrentDamage) - HazardSlowdownFactor * HazardManager.Instance.ActiveHazards.Count;
                speed = Mathf.Clamp(speed, 0, MaxSpeed);
                return speed;
            }
            else return 0f;
            
        }

        void Start()
        {
            configManager = FindObjectOfType<ConfigManager>();
            speedOverride = 0f;
            #region config
            HazardSlowdownFactor = configManager.config.HazardSlowDownFactor;
            #endregion

        }

        // Update is called once per frame
        void Update()
        {
            float newTargetSpeed = CalculateSpeed();
            if (newTargetSpeed != targetSpeed) //need to restart the speed shifting process
            {
                speedChangeProgress = 0f;
                targetSpeed = newTargetSpeed;
                startSpeed = currentSpeed;
            }

            if (currentSpeed != targetSpeed)
            {
                currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, speedChangeProgress);
                speedChangeProgress += Time.deltaTime;
                if (speedChangeProgress >= 1f) currentSpeed = targetSpeed;
            }
            
            distanceCovered += currentSpeed * Time.deltaTime;
        }
    }
}