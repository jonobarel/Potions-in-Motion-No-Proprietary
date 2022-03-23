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

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("CarriageModule"))
            {
                IsNearModule = true;
                nearModule = collision.gameObject.GetComponent<ModuleBase>();
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("CarriageModule"))
            {
                IsNearModule = false;
                nearModule = null;
                collision.gameObject.GetComponent<ModuleBase>().Interact(false, GetComponent<Dwarf>());
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
                if (IsNearModule && nearModule != null)
                {
                    if (context.performed)
                        nearModule.Interact(true, gameObject.GetComponent<Dwarf>());
                    else if (context.canceled)
                        nearModule.Interact(false, gameObject.GetComponent<Dwarf>());
                }
        }
    }
}