using System;
using System.Collections;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.UI;
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

        public bool TimedSpawning = true;
        
        private bool _isPaused = false;
        
        private HazardManager _hazardManager;
        private HazardSpawner _hazardSpawner;
        
        public Transform hazardDistanceSliderContainer;
        public Slider positionSliderPrefab;
        
        public ActiveHazards
        
        public void Start()
        {
            _hazardManager = new HazardManager();
            _hazardSpawner = new HazardSpawner(minTime, maxTime);
            if (TimedSpawning)
            {
                _hazardSpawner.StartSpawning(this);    
            }
            
        }

        public void Update()
        {
            
            _hazardManager.Update(Time.deltaTime * Mathf.Min(Carriage.Instance.CurrenSpeed,0.1f));
            //if should spawn but isn't
            if (TimedSpawning && !_hazardSpawner.IsSpawning())
            {
                _hazardSpawner.StartSpawning(this);
            }
            //if should not spawn but is still spawning
            else if (!TimedSpawning && _hazardSpawner.IsSpawning())
            {
                _hazardSpawner.StopSpawning();
            }
        }
    }
}