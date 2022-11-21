using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace ZeroPrep.MineBuddies
{
    public class MineBuddiesCharacter : MonoBehaviour
    {

        Character character;
        CharacterButtonActivation buttonActivator;
        public string PlayerLabel;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Character>();
            buttonActivator = GetComponent<CharacterButtonActivation>();
            //Destroy this object if it should not exist.
            if (VehicleDamageHandler.Instance != null) //we are in a play level - so destroy if shouldn't be.
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
        {/*
            var moduleButton = (ModuleButtonActivated)buttonActivator.ButtonActivatedZone;
            GameManager.HazardType type = moduleButton.Module.hazardType;
            GameSystem.Instance.analytics.LogEvent(character.PlayerID, Analytics.LogAction.UseModule, type, 1, "player activated module");*/
        }
    }
}