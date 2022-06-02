using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace com.baltamstudios.minebuddies
{
    public class MineBuddiesCharacter : MonoBehaviour
    {

        Character character;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Character>();
            //Destroy this object if it should not exist.
            if (Carriage.Instance != null) //we are in a play level - so destroy if shouldn't be.
            {
                if (!CharacterSelection.Instance.Participants.ContainsKey(GetComponent<Character>().PlayerID))
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LogModuleActivation()
        {
            throw new System.NotImplementedException();
        }
    }
}