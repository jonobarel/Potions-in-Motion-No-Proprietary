using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZeroPrep.MineBuddies
{
    public class FuelCanisterSpawn : MonoBehaviour
    {

        [FormerlySerializedAs("FuelCanisterPrefab")] public GameObject fuelCanisterPrefab;
        [FormerlySerializedAs("SpawnPoint")] public Transform spawnPoint;

        public void SpawnCanister()
        {
            var canisters = FindObjectOfType<FuelCanister>();
            if (canisters == null)
            {
                Debug.Log($"Spawning fuel canister at {spawnPoint.position}");
                GameObject fuelCanister = Instantiate(fuelCanisterPrefab);
                fuelCanister.GetComponent<Rigidbody2D>().MovePosition(spawnPoint.position);
            }
        }


        public void Update()
        {
            Debug.DrawLine(transform.position, spawnPoint.position);
        }
    }
}