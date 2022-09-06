using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class FuelCanisterSpawn : MonoBehaviour
    {

        public GameObject FuelCanisterPrefab;
        public Transform SpawnPoint;

        public void SpawnCanister()
        {
            var canisters = FindObjectOfType<FuelCanister>();
            if (canisters == null)
            {
                Debug.Log($"Spawning fuel canister at {SpawnPoint.position}");
                GameObject fuelCanister = Instantiate(FuelCanisterPrefab);
                fuelCanister.GetComponent<Rigidbody2D>().MovePosition(SpawnPoint.position);
            }
        }


        public void Update()
        {
            Debug.DrawLine(transform.position, SpawnPoint.position);
        }
    }
}