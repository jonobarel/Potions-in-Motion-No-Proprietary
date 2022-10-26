using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class TestSceneLogger : MonoBehaviour
    {
        /// <summary>
        /// This script is used for testing the HazardManager and Spawner objects in a test scene.
        /// Can be safely removed along with the scene itself when finished.
        /// </summary>
        // Start is called before the first frame update
        void Start()
        {
            HazardBase.OnSpawn += LogHazardSpawn;
            HazardBase.OnExpire += LogHazardExpire;
        }

        private void LogHazardSpawn(HazardBase h)
        {
            Debug.Log($"Spawned hazard: {h.Type}");
        }

        private void LogHazardExpire(HazardBase h)
        {
            Debug.Log($"Hazard expired: {h.Type}");
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}