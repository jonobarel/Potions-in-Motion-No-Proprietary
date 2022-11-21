using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroPrep.MineBuddies
{
    public class InfoSlidersUI : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        Slider carriageDamageSlider;
        [SerializeField]
        Slider carriageSpeedSlider;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (carriageDamageSlider != null)
            {
                carriageDamageSlider.value = VehicleDamageHandler.Instance.CurrentDamage;
            }
            else throw new System.NullReferenceException($"{name}: Carriage Damage Slider not set");
            if (carriageSpeedSlider != null)
            {
                float MaxSpeed = GameSystem.Instance.configManager.config.MaxCarriageSpeed;
                carriageSpeedSlider.value = VehicleDamageHandler.Instance.CarriageMovement.CurrentSpeed / MaxSpeed;
            }
            else throw new System.NullReferenceException($"{name}: Carriage Speed slider not set");
            
            
        }
    }
}