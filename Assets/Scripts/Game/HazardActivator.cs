using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardActivator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Hazard h = collision.GetComponent<Hazard>();
            if (h != null)
            {
                h.Activate();
                
                
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}