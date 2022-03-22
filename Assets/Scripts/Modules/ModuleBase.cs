using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public abstract class ModuleBase : MonoBehaviour
    {

        public IconDisplay icon;

        protected int upgradeLevel;
        public int UpgradeLevel { get { return upgradeLevel; } }

        protected void Upgrade()
        {
            Debug.Log($"{name}: upgraded");
        }

        public abstract bool Interact(); //call this function to make the machine work and subtract its power.

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