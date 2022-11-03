using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// This class is used to handle creating/spawning the UI element of the Hazard Sliders.
    /// </summary>
    public class HazardSliderUIManager
    {
        // ReSharper disable once InconsistentNaming
        private HazardSliderUIManagerGameObject _hazardTimeLineUI;


        
        #region ConstructorDestructor

        public HazardSliderUIManager(HazardSliderUIManagerGameObject hmGo)
        {
            _hazardTimeLineUI = hmGo.GetComponent<HazardSliderUIManagerGameObject>();
            HazardBase.Spawn += OnSpawn;
            HazardBase.Clear += OnClear;
            HazardBase.Expire += OnExpire;
        }

        ~HazardSliderUIManager()
        {
            HazardBase.Spawn -= OnSpawn;
            HazardBase.Clear -= OnClear;
            HazardBase.Expire -= OnExpire;
        }
        #endregion 
        
        #region EventMethods
        private void OnExpire(HazardBase h)
        {
            throw new System.NotImplementedException();
        }

        private void OnClear(HazardBase h)
        {
            throw new System.NotImplementedException();
        }
        private void OnSpawn(HazardBase h)
        {
            //Instantiate the UI element and
            //add it to the timeline

            Transform sliderContainer = _hazardTimeLineUI.Slidercontainer;
            Slider prefab = _hazardTimeLineUI.ProgressSliderPrefab;

            Slider positionSlider = Object.Instantiate(prefab, sliderContainer);
            
            
            /*positionSlider = Instantiate(hazardManager.PositionSliderPrefab, hazardManager.HazardDistanceSliderContainer);
            positionSlider.GetComponent<HazardSliderDisplay>().HazardMono = this;
            */
        }
        
        #endregion
    }
}