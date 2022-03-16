using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    

    public class Dwarf : MonoBehaviour
    {
        
        // Start is called before the first frame update
        public Color[] CharacterColours = { Color.red, Color.green, Color.blue, Color.yellow };
        private CharacterMove characterMove;

        // Update is called once per frame


        public void Start()
        {
            characterMove = GetComponent<CharacterMove>();
        }
        public void SetColor(Color c)
        {
            Renderer r = GetComponent<Renderer>();
            r.material.color = c;
        }

        private void OnTriggerEnter(Collider other)
        {
            //upon entering the carriage, switch the control scheme.
            if (other.CompareTag("CarriageVolume")) {
                characterMove.InsideCarriage = true;
                Debug.Log($"{name} Entered carriage");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CarriageVolume"))
            {
                characterMove.InsideCarriage = false;
                Debug.Log($"{name} exited carriage");
            }
        }
    }

    
}