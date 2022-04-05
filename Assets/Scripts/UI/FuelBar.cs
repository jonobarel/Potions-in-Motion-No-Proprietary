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
        Slider fuelBarImage;

        void Update()
        {
            fuelBarImage.value = Carriage.Instance.Engine.FuelLevel;
        }
    }
}