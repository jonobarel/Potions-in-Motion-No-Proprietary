using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class ModuleBase : MonoBehaviour
    {

        protected float powerLevel;
        public float PowerLevel { get { return powerLevel; } }
        protected int upgradeLevel;
        public int UpgradeLevel { get { return upgradeLevel; } }

        public Resource resourceType; //resource consumed by this Module
        protected float ActionPower;  //power requirement
        public IconDisplay icon;
        public int ResourceLevel { get { return resourceLevel; } } //quantity of Resources available.
        protected int resourceLevel;

        public bool Interact() //call this function to make the machine work and subtract its power.
        {
            if (ActionPower > PowerLevel) //not enough power
            {
                Debug.Log($"{name}: insufficient power.");
                return false;
            }
            if (resourceLevel == 0)
            {
                Debug.Log($"{name}: insuffience resources of type {typeof(Resource).Name}");
            }

            if (DoAction())
            {
                powerLevel -= ActionPower;
                return true;
            }
            return false;
        }

        protected bool DoAction() //override this method to implement the specific action the module does.
        {
            Debug.Log($"{name}: action performed.");
            return true;
        }

        protected void Upgrade()
        {
            Debug.Log($"{name}: upgraded");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"{other.name} approached {name}");
                icon.Show();
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) {
                Debug.Log($"{other.name} moving away from {name}");
                icon.Hide(); }

        }
    }
}