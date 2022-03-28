using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Hazard : MonoBehaviour
    {
        public GameManager.HazardType type;
        float initialDuration = 10f;
        float hazardProgress = 0f;

        float HazardProgessionRate = 1f;
        float FixProgressionRate = 0.1f;

        float DifficultyFactor = 1f;

        public float InitialDuration { get { return InitialDuration; } }
        
        public float TimeRemaining { get { return InitialDuration - hazardProgress; } }

        float fixProgress = 0f;
        public float FixProgress { get { return fixProgress; } }

        public void SetDuration(float d)
        {
            if (d == 0f)
            {
                throw new System.IndexOutOfRangeException($"{name}: Hazard duration cannot be 0 to avoid divide by 0 error");
            }
            initialDuration = d;
            hazardProgress = 0f;
        }
    }
}