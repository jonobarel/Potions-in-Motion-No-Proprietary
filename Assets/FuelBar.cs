using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baltamstudios.minebuddies
{
    public class FuelBar : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        Image fuelBarImage;

        void Update()
        {
            fuelBarImage.fillAmount = Carriage.Instance.Engine.Fuel;
        }
    }
}