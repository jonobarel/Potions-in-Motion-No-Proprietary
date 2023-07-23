using System;
using System.Linq;
using UnityEngine;
using static ZeroPrep.Utils.Helpers;
using Random = UnityEngine.Random;


namespace ZeroPrep.MineBuddies
{/// <summary>
 /// Spawns hazards with 2 hazard types (class <see cref="HazardMultiType"/>)
 /// </summary>
    public class HazardSpawnerMultiType : HazardSpawner
    {
        private Managers.HazardType[] _shuffledTypes;
        
        
        public HazardSpawnerMultiType(float minTime, float maxTime, AvailableHazardTypes validTypes, MineBuddiesMultiplayerLevelManager levelManager) : base(minTime, maxTime, validTypes, levelManager)
        {
            _shuffledTypes = new Managers.HazardType[Types.Length];
            Types.CopyTo(_shuffledTypes,0);
        }

        /// <summary>
        /// Spawns a multitype hazard based on complexity.
        /// If complexity is 2 or less, it will spawn a regular hazard with 1 type.
        /// </summary>
        /// <param name="complexity">complexity level of spawned hazard. Default = 2 (Regular Hazard)</param>
        /// <returns>spawned hazard</returns>
        public HazardBase SpawnRandomTypeHazard(int complexity = 2)
        {
            Shuffle(_shuffledTypes);
            complexity = Math.Max(2, complexity);
            int numSegments = Random.Range(1, complexity);
            int numTypes = complexity - numSegments;
            
            var subset = _shuffledTypes.Take(numTypes);
            Managers.HazardType[] hazardTypes = subset.ToArray();
            HazardManagerGO.InteractionType[] interactionsForHazard = new HazardManagerGO.InteractionType[hazardTypes.Count()];
            for (int i = 0; i < interactionsForHazard.Length; i++)
            {
                interactionsForHazard[i] = _interactions[Random.Range(0, _interactions.Length - 1)];
            }
            
            HazardMultiType hazardMultiType = new HazardMultiType(Speed, hazardTypes, interactionsForHazard, numSegments);

            return hazardMultiType;

        }
    }
}