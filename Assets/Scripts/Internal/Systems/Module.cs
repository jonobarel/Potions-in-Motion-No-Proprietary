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
        private Engine _engine;
        private Managers.HazardType _hazardType;
        private HazardManager _hazardManager;

        // Start is called before the first frame update
        void Start()
        {
        }

        [Inject]
        public void Init(Engine engine)
        {
            if (engine == null)
            {
                throw new ArgumentException();
            }
            _engine = engine;
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