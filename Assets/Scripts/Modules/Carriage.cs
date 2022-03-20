using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Carriage : MonoBehaviour
    {
        
        
        private static Carriage instance;
        public static Carriage Instance
        {
            get {  
                if (instance == null) { 
                    instance = FindObjectOfType<Carriage>(); 
                } 
                return instance;
            }
        }

        
        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(this.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

      
    }
}