using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Carriage : MonoBehaviour
    {

        PowerModule engine;
        float currentDamage = 0f;
        CarriageMovement carriageMovement;
        public CarriageMovement CarriageMovement { get { return carriageMovement; } }

        public float CurrentDamage { get { return currentDamage; } }
        
        public PowerModule Engine { get { return engine; } }

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
            if (engine == null)
                engine = FindObjectOfType<PowerModule>();
            carriageMovement = GetComponent<CarriageMovement>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

      
    }
}