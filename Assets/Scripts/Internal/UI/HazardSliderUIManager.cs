using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// This class is used to handle creating/spawning the UI element of the Hazard Sliders.
    /// </summary>
    public class HazardSliderUIManager
    {
        // ReSharper disable once InconsistentNaming
        private HazardTimeLineUI _hazardTimeLineUI;

        private Dictionary<HazardBase, GameObject> _sliders;
        
        #region ConstructorDestructor

        public HazardSliderUIManager(HazardTimeLineUI hmGo)
        {
            _hazardTimeLineUI = hmGo;
            HazardBase.OnSpawn += AddHazardToTimeline;
            HazardBase.OnClear += HazardCleared;
            HazardBase.OnExpire += HazardExpired;
        }

        ~HazardSliderUIManager()
        {
            HazardBase.OnSpawn -= AddHazardToTimeline;
        }
        #endregion 
        
        #region EventMethods
        private void HazardExpired(HazardBase h)
        {
            throw new System.NotImplementedException();
        }

        private void HazardCleared(HazardBase h)
        {
            throw new System.NotImplementedException();
        }
        private void AddHazardToTimeline(HazardBase h)
        {
            //Instantiate the UI element and
            //add it to the timeline
            
            throw new System.NotImplementedException();
        }
        
        #endregion
    }
}