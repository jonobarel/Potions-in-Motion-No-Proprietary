using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class PowerModule : ModuleBase
    {

        [SerializeField]
        float MaxFuel { get { return GameSystem.Instance.configManager.config.FuelCapacity;  } }
        [SerializeField]
        float FuelBurnRate { get { return GameSystem.Instance.configManager.config.FuelBurnRateFactor; } }  //fuel consumption per second per power unit
        [SerializeField]
        float FuelPerUnit { get { return GameSystem.Instance.configManager.config.RefuelSize; } } // the amount of fuel added to the engine for each Refueling resource.
        [SerializeField]
        int MaxConnections = 4;

        public float PowerDemand
        {
            get { return (float)connectedModules.Count/MaxConnections; }
        }
        [SerializeField]
        List<ActionModule> connectedModules = new List<ActionModule>();
        [SerializeField]
        float fuel;

        bool isRefueling;
        float refuelCooldownStatus;

        Coroutine refuelRoutine;

        public float FuelLevel { get { return fuel/MaxFuel; } }

        void Start()
        {
            //TODO figure out a different way to set the starting fuel.
            fuel = MaxFuel;
        }

        // Update is called once per frame
        void Update()
        {
            
            if (fuel > 0)
            {
                fuel -= FuelBurnRate * (1+connectedModules.Count) * Time.deltaTime;
            }
        }


        void AddFuel(float units)
        {
            fuel += units * FuelPerUnit;
            fuel = Mathf.Clamp(fuel, 0, MaxFuel);
        }

        public override void Interact(bool isStart, Dwarf player)
        {
            if (isStart)
            {
                if (!isRefueling && refuelCooldownStatus == 0)
                {
                    DoRefuel();
                }
                else Debug.Log($"{ player.name}: cannot refule. IsRefueling: {isRefueling}, cooldownTimer: {refuelCooldownStatus}s");

            }
        }

        public void DoRefuel()
        {
            if (!isRefueling)
            {
                isRefueling = true;
                float startFuelLevel = fuel;
                float targetFuelLevel = Mathf.Min(fuel + FuelPerUnit, MaxFuel);
                refuelRoutine = this.CreateAnimationRoutine(
                    GameSystem.Instance.configManager.config.RefuelTime,
                    delegate (float progress)
                    {
                        fuel = Mathf.Lerp(startFuelLevel, targetFuelLevel, progress);
                    },
                    delegate()
                    {
                        fuel = targetFuelLevel;
                        isRefueling=false;
                        refuelCooldownStatus = GameSystem.Instance.configManager.config.RefuelCooldownTime;
                    }
                    );

            }
        }
    }
}