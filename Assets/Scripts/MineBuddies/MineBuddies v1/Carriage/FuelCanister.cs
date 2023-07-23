using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class FuelCanister : MonoBehaviour
    {
        bool _setToRefuel = false;
        //PowerModule _powerModule;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("PowerModule"))
            {
          //      _powerModule = collision.gameObject.GetComponent<PowerModule>();
                _setToRefuel = true;
            }
        }

        public void Update()
        {
            if (_setToRefuel)
            {
                //_powerModule.DoRefuel();
                MoreMountains.CorgiEngine.Pushable pushable = GetComponent<MoreMountains.CorgiEngine.Pushable>();
                pushable.Detach(pushable.Pusher);
                GameObject.Destroy(gameObject);
            }
            
        }
    }
}