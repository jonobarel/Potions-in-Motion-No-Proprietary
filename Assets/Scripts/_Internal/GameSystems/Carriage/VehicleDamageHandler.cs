using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZeroPrep.MineBuddies
{
    public class VehicleDamageHandler : MonoBehaviour
    {

        
        private void OnHazardExpired(HazardBase h)
        {
            throw new NotImplementedException("Carriage detected hazard expired");
        }

        private void OnHazardCleared(HazardBase h)
        {
            throw new NotImplementedException("Carriage detected hazard cleared");
        }

        public void Start()
        {
            HazardBase.Expire += OnHazardExpired;
            HazardBase.Clear += OnHazardCleared;
        }

        public void OnDestroy()
        {
            HazardBase.Expire -= OnHazardExpired;
            HazardBase.Clear -= OnHazardCleared;

        }
    }
}