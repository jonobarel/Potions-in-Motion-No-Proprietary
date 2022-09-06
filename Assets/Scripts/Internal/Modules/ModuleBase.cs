using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZeroPrep.MineBuddies
{
    public abstract class ModuleBase : MonoBehaviour
    {

        public Sprite icon;

        protected int upgradeLevel;
        public int UpgradeLevel { get { return upgradeLevel; } }

        protected void Upgrade()
        {
            Debug.Log($"{name}: upgraded");
        }

        public abstract void Interact(bool isStart, Dwarf player); //call this function to make the machine work and subtract its power.

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Debug.Log($"{collision.gameObject.name} approached {name}");
            }
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Debug.Log($"{collision.gameObject.name} moving away from {name}");
            }
        }
    }
}