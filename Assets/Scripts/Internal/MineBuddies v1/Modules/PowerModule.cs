using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class PowerModule : ModuleBase
    {

        [SerializeField]
        float MaxFuel { get { if (GameSystem.Instance) return GameSystem.Instance.configManager.config.FuelCapacity;
                return 100f; //placeholder value for testing in scenes
            } }
        [SerializeField]
        float FuelBurnRate { get { if (GameSystem.Instance) return GameSystem.Instance.configManager.config.FuelBurnRateFactor;
                return 10f; //placeholder value for testing in scenes.
            } }  //fuel consumption per second per power unit
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
        List<Module> connectedModules = new List<Module>();
        [SerializeField]
        float fuel;
        public float CurrentFuel { get { return fuel; } }

        bool isRefueling = false;
        float refuelCooldownStatus;

        Coroutine refuelRoutine;

        public float FuelLevel { get { return fuel/MaxFuel; } }

        void Start()
        {
            //TODO figure out a different way to set the starting fuel.
            if (GameSystem.Instance != null)
            {
                fuel = MaxFuel;
            }
            else fuel = 100f;

            var fuelBar = FindObjectOfType<FuelBar>();
            if (fuelBar != null)
            {
                fuelGuage = fuelBar.GetComponent<MoreMountains.Tools.MMProgressBar>();
                fuelGuage.SetBar01(FuelLevel);
            }
            
            
        }

        // Update is called once per frame
        void Update()
        {
            
            if (fuelGuage && fuel > 0)
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


        public void DoRefuel()
        {
            if (Helpers.Config && fuelGuage)
            {
                fuel += Helpers.Config.RefuelSize;
                fuel = Mathf.Min(fuel, MaxFuel);
                fuelGuage.UpdateBar01(fuel / MaxFuel);
            }
            

        }
    }
}