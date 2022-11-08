using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    [Serializable]
    public class GameSettings
    {
        [Header("Engine Properties")]
        public float EngineCapacity = 100f;

        public float EngineStartingFuel = 100f;

        public float EngineBurnRate = 10f;
    }
}