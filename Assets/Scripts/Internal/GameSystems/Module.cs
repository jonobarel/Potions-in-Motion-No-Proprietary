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
        [Inject]
        private GameSettings _gameSettings;
        
        [SerializeField]
        private Engine _engine;
        public Engine Engine => _engine;
    
        [SerializeField]
        private bool IsInitialized = false;
        
        private Managers.HazardType _hazardType;
        private HazardManager _hazardManager;

        // Start is called before the first frame update
        void Start()
        {
        }

                
        [Inject]
        public void Init(Engine engine)
        {
            if (engine != null)
            {
                _engine = engine;
            }
            else
            {
                throw new ArgumentException();
            }

            IsInitialized = true;
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