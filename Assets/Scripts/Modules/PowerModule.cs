using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class PowerModule : ModuleBase
    {

        [SerializeField]
        float MaxFuel = 10f;
        [SerializeField]
        float PowerUnitBurnRate = 0.05f; //fuel consumption per second per power unit
        [SerializeField]
        float FuelPerUnit = 2.5f; // the amount of fuel added to the engine for each Refueling resource.
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

        public float Fuel { get { return fuel/MaxFuel; } }

        int powerUnitsCapacity = 5;

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
                fuel -= PowerUnitBurnRate * connectedModules.Count * Time.deltaTime;
                foreach(ActionModule module in connectedModules)
                {
                    module.HasPower = true;
                }
            }
            else
            {
                foreach (ActionModule module in connectedModules)
                {
                    module.HasPower = false ;
                }
            }

        }

        public void Connect(ActionModule actionModule)
        {
            connectedModules.Add(actionModule);
            actionModule.IsConnected = true;

        }

        public void Disconnect(ActionModule actionModule)
        {
            connectedModules.Remove(actionModule);
            actionModule.IsConnected = false;
            actionModule.HasPower = false;
        }

        void AddFuel(float units)
        {
            fuel += units * FuelPerUnit;
            fuel = Mathf.Clamp(fuel, 0, MaxFuel);
        }

        public override void Interact(bool isStart, Dwarf player)
        {
            throw new System.NotImplementedException();
        }
    }
}