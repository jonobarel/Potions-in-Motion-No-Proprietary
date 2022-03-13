using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.baltamstudios.minebuddies
{
    

    public class Dwarf : MonoBehaviour
    {
        // Start is called before the first frame update
        public Color[] CharacterColours = { Color.red, Color.green, Color.blue, Color.yellow };
       

        // Update is called once per frame



        public void SetColor(Color c)
        {
            Renderer r = GetComponent<Renderer>();
            r.material.color = c;
        }
    }
}