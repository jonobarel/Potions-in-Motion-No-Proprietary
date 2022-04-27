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

        MoreMountains.Tools.MMProgressBar fuelGuage;
        public float PowerDemand
        {
            get { return (float)connectedModules.Count/MaxConnections; }
        }
        [SerializeField]
        List<ActionModule> connectedModules = new List<ActionModule>();
        [SerializeField]
        float fuel;
        public float CurrentFuel { get { return fuel; } }

        bool isRefueling;
        float refuelCooldownStatus;

        Coroutine refuelRoutine;

        public float FuelLevel { get { return fuel/MaxFuel; } }

        void Start()
        {
            //TODO figure out a different way to set the starting fuel.
            fuel = MaxFuel;
            fuelGuage = GetComponentInChildren<MoreMountains.Tools.MMProgressBar>();
            fuelGuage.SetBar01(FuelLevel);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (fuel > 0)
            {
                fuel -= FuelBurnRate * Time.deltaTime;
                fuelGuage.SetBar01(FuelLevel);
            }

        }

        public bool ModuleFuel()
        {
            if (fuel > Helpers.Config.ModuleFuelConsumption)
            {
                fuel-=Helpers.Config.ModuleFuelConsumption;
                UpdateFuelGuage();
                return true;
            }
            return false;
        }

        void UpdateFuelGuage()
        {
            fuelGuage.UpdateBar01(FuelLevel);
        }
        public void AddFuel(float units)
        {
            fuel += units * FuelPerUnit;
            fuel = Mathf.Clamp(fuel, 0, MaxFuel);
            fuelGuage.UpdateBar01(FuelLevel);
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
            fuel += Helpers.Config.RefuelSize;
            fuel = Mathf.Min(fuel, MaxFuel);
            fuelGuage.UpdateBar01(fuel / MaxFuel);

        }
    }
}