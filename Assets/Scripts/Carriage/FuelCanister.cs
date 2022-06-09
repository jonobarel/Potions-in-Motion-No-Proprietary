using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class FuelCanister : MonoBehaviour
    {
        bool setToRefuel = false;
        PowerModule powerModule;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("PowerModule"))
            {
                powerModule = collision.gameObject.GetComponent<PowerModule>();
                setToRefuel = true;
            }
        }

        public void Update()
        {
            if (setToRefuel)
            {
                powerModule.DoRefuel();
                MoreMountains.CorgiEngine.Pushable pushable = GetComponent<MoreMountains.CorgiEngine.Pushable>();
                pushable.Detach(pushable.Pusher);
                GameObject.Destroy(gameObject);
            }
            
        }
    }
}