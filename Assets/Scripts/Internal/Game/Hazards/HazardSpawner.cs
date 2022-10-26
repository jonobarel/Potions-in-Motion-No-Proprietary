using System;
using System.Collections;
using UnityEngine;
using ZeroPrep.MineBuddies;
using Random = UnityEngine.Random;

namespace ZeroPrep.MineBuddies
{
    public class HazardSpawner
    {
        public float MinTime { get; }

        public float MaxTime { get; }

        public bool IsPaused { get; }

        private bool _stopSpawning = false;

        
        private Array _types = Enum.GetValues((typeof(Managers.HazardType)));

        /*
        Array values = Enum.GetValues(typeof(Bar));
        Random random = new Random();
        Bar randomBar = (Bar)values.GetValue(random.Next(values.Length));
        */
        #region Constructor

        public HazardSpawner(float minTime, float maxTime)
        {
            MinTime = minTime;
            MaxTime = maxTime;
        }

        #endregion
        
        private IEnumerator SpawnNextHazardCoRoutine()
        {
            while (!_stopSpawning)
            {
                while (IsPaused)
                {
                    yield return null;
                }

                float delay = Random.Range(MinTime, MaxTime);
                yield return new WaitForSeconds(delay);
                
                //spawn!
                SpawnRandomTypeHazard();
            }

            // removed due to triviality, but make sure to test this
            //_stopSpawning = false;
        }

        public void StopSpawning()
        {
            _stopSpawning = true;
        }

        public void StartSpawning(MonoBehaviour caller)
        {
            _stopSpawning = false;
            caller.StartCoroutine(SpawnNextHazardCoRoutine());
        }
        
        public void SpawnRandomTypeHazard()
        {
            Managers.HazardType newType = (Managers.HazardType)_types.GetValue(Random.Range(0, _types.Length));
            HazardExternal h = new HazardExternal(0.5f, newType);
        }

        public bool IsSpawning()
        {
            return !_stopSpawning;
        }
        
    }
}