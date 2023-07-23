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
        private HazardType[] _shuffledTypes;
        
        
        public HazardSpawnerMultiType(float minTime, float maxTime, HazardTypesActiveInGame validTypesActiveInGame, MineBuddiesMultiplayerLevelManager levelManager) : base(minTime, maxTime, validTypesActiveInGame, levelManager)
        {
            _shuffledTypes = new HazardType[Types.Length];
            Types.CopyTo(_shuffledTypes,0);
        }

        /// <summary>
        /// Spawns a multitype hazard based on complexity.
        /// If complexity is 2 or less, it will spawn a regular hazard with 1 type.
        /// </summary>
        /// <param name="complexity">complexity level of spawned hazard. Default = 2 (Regular Hazard)</param>
        /// <param name="numTypes">Force number of <see cref="HazardType"/> to be <c>numTypes</c>. If 0, random number of HazardTypes will be chosen</param>
        /// <param name="numSegments">Force number of segments.</param>
        /// <returns>spawned hazard</returns>
        public HazardBase SpawnRandomTypeHazard(int complexity = 2, int numTypes = 0, int numSegments = 0)
        {
            Shuffle(_shuffledTypes);
            complexity = Math.Max(2, complexity);
            if (numTypes > 0)
            {
                numSegments = complexity - numTypes;
            }
            else if (numSegments > 0)
            {
                numTypes = complexity - numSegments;
            }
            else
            {
                numTypes = Random.Range(1, complexity - 1);
                numSegments = complexity - numTypes;
            }

            var subset = _shuffledTypes.Take(numTypes);
            HazardType[] hazardTypes = subset.ToArray();
            HazardManagerGO.InteractionType[] interactionsForHazard =
                new HazardManagerGO.InteractionType[hazardTypes.Count()];
            for (int i = 0; i < interactionsForHazard.Length; i++)
            {
                interactionsForHazard[i] = _interactions[Random.Range(0, _interactions.Length - 1)];
            }

            HazardMultiType hazardMultiType =
                new HazardMultiType(Speed, hazardTypes, interactionsForHazard, numSegments);

            return hazardMultiType;

        }
    }
}