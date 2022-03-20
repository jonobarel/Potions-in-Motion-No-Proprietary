using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class ModulePower : ModuleBase
    {

        [SerializeField]
        float MaxFuel = 10f;
        [SerializeField]
        float PowerUnitBurnRate = 0.05f; //fuel consumption per second per power unit
        [SerializeField]
        float FuelPerUnit = 2.5f; // the amount of fuel added to the engine for each Refueling resource.
        [SerializeField]
        int PowerDemand = 0;

        Gauge gauge;

        float fuel;

        int powerUnitsCapacity = 5;

        void Start()
        {
            //TODO figure out a different way to set the starting fuel.
            fuel = MaxFuel;
            gauge = GetComponentInChildren<Gauge>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (fuel > 0)
            {
                fuel -= PowerUnitBurnRate * PowerDemand*Time.deltaTime;
            }

            gauge.Position = fuel / MaxFuel;
        }
        void AddFuel(int units)
        {
            fuel += units * FuelPerUnit;
            fuel = Mathf.Clamp(fuel, 0, MaxFuel);
        }
    }
}