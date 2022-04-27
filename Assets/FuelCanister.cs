using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class FuelCanister : MonoBehaviour
    {

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("PowerModule"))
            {
                PowerModule powerModule = collision.gameObject.GetComponent<PowerModule>();
                powerModule.DoRefuel();
                MoreMountains.CorgiEngine.Pushable pushable = GetComponent<MoreMountains.CorgiEngine.Pushable>();
                pushable.Detach(pushable.Pusher);
                GameObject.Destroy(gameObject, 0.5f);
            }
        }
    }
}