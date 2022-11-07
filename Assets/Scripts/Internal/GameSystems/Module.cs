using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;

namespace ZeroPrep.MineBuddies
{


    public class Module : MonoBehaviour
    {
        [SerializeField]
        private Engine _engine;
        public Engine Engine => _engine;
    
        
        private Managers.HazardType _hazardType;
        private HazardManager _hazardManager;

        // Start is called before the first frame update
        void Start()
        {
        }

                
        [Inject]
        public void Construct(Engine engine)
        {
            if (engine != null)
            {
                _engine = engine;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        
        public void Interact(GameObject actor)
        {
            throw new NotImplementedException();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}