using System;
using System.Linq;
using static ZeroPrep.Utils.Helpers;
using Random = UnityEngine.Random;


namespace ZeroPrep.MineBuddies
{/// <summary>
 /// Spawns hazards with 2 hazard types (class <see cref="HazardMultiType"/>)
 /// </summary>
    public class HazardSpawnerMultiType : HazardSpawner
    {
        private Managers.HazardType[] _shuffledTypes;
        
        /// <summary>
        /// Number of segments in the <see cref="HazardMultiType"/> (i.e., it will flip <c>(Segments-1)</c> times during treatment)
        /// </summary>
        public int Segments => 4;

        
        public HazardSpawnerMultiType(float minTime, float maxTime, AvailableHazardTypes validTypes, MineBuddiesMultiplayerLevelManager levelManager) : base(minTime, maxTime, validTypes, levelManager)
        {
            _shuffledTypes = new Managers.HazardType[Types.Length];
            Types.CopyTo(_shuffledTypes,0);
        }

        public override HazardBase SpawnRandomTypeHazard()
        {
            Shuffle(_shuffledTypes);
            var subset = _shuffledTypes.Take(2);
            Managers.HazardType[] hazardTypes = subset.ToArray();
            HazardManagerGO.InteractionType[] interactionsForHazard = new HazardManagerGO.InteractionType[hazardTypes.Count()];
            for (int i = 0; i < interactionsForHazard.Length; i++)
            {
                interactionsForHazard[i] = _interactions[Random.Range(0, _interactions.Length - 1)];
            }
            
            HazardMultiType hazardMultiType = new HazardMultiType(Speed, hazardTypes, interactionsForHazard, Segments);

            return hazardMultiType;

        }
    }
}