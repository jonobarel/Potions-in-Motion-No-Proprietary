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
                GameObject fuelCanister = Instantiate(FuelCanisterPrefab, SpawnPoint);
                fuelCanister.GetComponent<Rigidbody2D>().MovePosition(SpawnPoint.position);

        }

    }
}