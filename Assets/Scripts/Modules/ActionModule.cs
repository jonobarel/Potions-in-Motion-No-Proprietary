using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public abstract class ActionModule : ModuleBase
    {
        protected float powerLevel;
        public float PowerLevel { get { return powerLevel; } }


        [SerializeField]
        public Resource resourceType; //resource consumed by this Module
        [SerializeField]
        protected float actionPowerCost;  //power requirement
        
        public int resourceLevel; //quantity of Resources available.

        PowerModule Engine { get { return Carriage.Instance.Engine; } }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}