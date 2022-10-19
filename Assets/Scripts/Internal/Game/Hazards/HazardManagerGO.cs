using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// The <c>HazardSpawner</c> is a gameobject responsible for generating Hazards.
    /// It exists in UnityEngine because it needs to be integrated for Time, frames, and pauses (if and when they happen).
    /// </summary>
    public class HazardManagerGO : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f,5f)]
        private float minTime = 0.5f;
        [SerializeField]
        [Range(0.1f, 5f)]
        private float maxTime = 1.5f;

        private bool _isPaused = false;

        private HazardManager _hazardManager = new HazardManager();
        private HazardSpawner _hazardSpawner;
        
        public void Start()
        {
            _hazardSpawner = new HazardSpawner(minTime, maxTime);
        }

        public void Update()
        {
            
        }
        
    }
}