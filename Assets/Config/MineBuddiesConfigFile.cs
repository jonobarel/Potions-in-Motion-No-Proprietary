using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.baltamstudios.minebuddies
{
    [CreateAssetMenu(fileName = "MineBuddiesConfig", menuName = "MineBuddies Config Asset", order = 0)]
    public class MineBuddiesConfigFile : ScriptableObject
    {
        #region Carriage parames
        [Header("Carriage parameters")]
        public float MaxCarriageSpeed;
        public float HazardSlowDownFactor = 0.1f;
        

        #endregion
        [Space]

        [Header("Hazard Parameters")]
        #region Hazard params
        [Range(0f, 150f)]
        public float HazardStartingDistance = 100f;
        public float InitialDuration = 10f;
        public float HazardProgressionRate = 1f;
        [Space]
        [Range(0f,2f)]
        public float HazardFixProgressionRate = 0.1f;
        public float HazardDifficultyFactor = 1f;
        #endregion

        [Space]

        #region Player Config
        [Header("Player")]
        public float GroundDetection = 0.15f;
        public float GravityScale = 3f;
        public float MaxFallSpeed = 10f;
        public float MaxHorizontalVelocity = 5f;

        public float SmoothTime = 0.25f;
        public float JumpForce = 3.5f;
        public float ClimbSpeed = 3f;

        public float LadderWalkThreshold = 0.92f;
        public float LadderClimbThreshold = 0.45f;
        #endregion
    }
}