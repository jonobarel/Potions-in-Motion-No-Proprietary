using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baltamstudios.minebuddies
{
    public class Hazard : MonoBehaviour
    {
        public GameManager.HazardType type;
        public Slider distanceSlider;        

        [Header("Hazard countdown fields")]
        [SerializeField]
        float initialDuration = 10f;
        [SerializeField]
        float timeRemaining;
        [SerializeField]
        float HazardProgessionRate = 1f;
        ActionModule module;
        
        [Space]
        [Header("Hazard fixing progression")]
        [SerializeField, Range(0,1f)]
        float fixProgress = 0f;
        [SerializeField]
        float FixProgressionRate = 0.1f;
        
        float DifficultyFactor = 1f;

        #region properties
        public float InitialDuration { get { return initialDuration; } }
        public float TimeRemaining { get { return timeRemaining; } }
        public float FixProgress { get { return fixProgress; } }
        #endregion

        [SerializeField]
        bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; } }


        public void Update()
        {
            if (isActive)
            {
                if (timeRemaining > 0)
                {
                    //proceed with the coundtdown  until reaches zero
                    timeRemaining -= HazardProgessionRate * Time.deltaTime;
                    timeRemaining = Mathf.Max(timeRemaining, 0);

                    if (module != null && module.IsWorking) {
                        fixProgress += FixProgressionRate * Time.deltaTime;
                        fixProgress = Mathf.Max(fixProgress, 1f);
                    }
                }
                if (timeRemaining <=0f || fixProgress >= 1f) // timeremaining <=0
                {
                    isActive = false;
                }
            }

        }

        internal void AnimateBecomesActive()
        {
            throw new NotImplementedException();
        }

        internal void AnimateReachedZero()
        {
            throw new NotImplementedException();
        }

        public void SetDuration(float d)
        {
            if (d == 0f)
            {
                throw new System.IndexOutOfRangeException($"{name}: Hazard duration cannot be 0 to avoid divide by 0 error");
            }
            initialDuration = d;
            timeRemaining = d;
        }
        public void SetType(GameManager.HazardType t)
        {
            type = t;
            module = GameSystem.Instance.hazardManager.HazardModuleMap[type];
        }

        public void ApplyFix(float fixTime)
        {
            if (fixProgress < 1f)
            {
                fixProgress += fixTime * FixProgressionRate * DifficultyFactor;
                fixProgress = Mathf.Min(fixProgress, 1f);
            }
        }

    }
}