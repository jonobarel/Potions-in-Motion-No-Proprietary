using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class DebugVectors : MonoBehaviour
    {
        Vector3 impulseVector = Vector3.zero;
        float debugStart = 0f;
        public float DisplayDuration = 1.5f;
        public float DisplayScale = 100f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (impulseVector != Vector3.zero)
            {

            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"{name} collided with {collision.gameObject.name}");
            impulseVector = collision.impulse;
            Debug.DrawRay(transform.position, DisplayScale * collision.impulse, Color.yellow, DisplayDuration);
            debugStart = Time.time;
        }
    }
}