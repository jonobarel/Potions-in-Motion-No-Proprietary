using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.baltamstudios.minebuddies
{
    public class ModuleMouseInteraction : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private ModuleBase module;

        public void Start()
        {
            module = GetComponent<ModuleBase>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            module.Interact();
        }
    }
}