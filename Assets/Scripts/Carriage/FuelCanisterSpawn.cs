using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
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
                GameObject fuelCanister = Instantiate(FuelCanisterPrefab);
                fuelCanister.GetComponent<Rigidbody2D>().MovePosition(SpawnPoint.position);
            }
        }

    }
}