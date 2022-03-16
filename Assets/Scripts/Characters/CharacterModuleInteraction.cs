using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    public class CharacterModuleInteraction : MonoBehaviour
    {
        [SerializeField]
        bool IsNearModule = false;
        [SerializeField]
        ModuleBase nearModule;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CarriageModule"))
            {
                IsNearModule = true;
                nearModule = other.GetComponent<ModuleBase>();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CarriageModule"))
            {
                IsNearModule = false;
                nearModule = null;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log($"{name} interacting");
            var str = IsNearModule ? nearModule.name : "no";
            Debug.Log($"Near module: {str}");
            if (IsNearModule && nearModule != null)
            {
                    nearModule.Interact();
            }
            
        }
    }
}