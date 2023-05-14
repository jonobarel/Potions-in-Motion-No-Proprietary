using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ZeroPrep.MineBuddies.SpawnableListUtils;
using Random = UnityEngine.Random;

namespace ZeroPrep.MineBuddies
{
    public class ProbabilitySpawner : Spawner
    {
        public float PointsGrowthRate = 1.1f;
        public int StartingWavePointValue { get; protected set; }
        public int NextWavePointValue { get; protected set; }

        protected virtual int CalculateNextWavePointValue(int currentValue)
        {
            return currentValue + CurrentWaveIndex;
        }

        
        public ProbabilitySpawner(List<ISpawnable> units, int startingSize, int startingPointValue) : base(units, startingSize)
        {
            StartingWavePointValue = startingPointValue;
            NextWavePointValue = startingPointValue;
        }

        
        
        private List<ISpawnable> BuildWaveFromPoints(int points)
        {
            List<ISpawnable> pool = Units.Where(spawnable => spawnable.PointValue < points).ToList();

            if (pool.Count == 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"Could not build wave: no qualifying units. Points: {points}. Lowest point value: {Units.Min(u => u.PointValue)}");
            }
            
            float pointPool = pool.Sum(spawnable => spawnable.PointValue);
           
            List<ISpawnable> wave = new List<ISpawnable>();

            while (points > 0 && pool.Count > 0)
            {
                float probabilityPool = pool.Sum(spawnable => spawnable.Probability);
                ISpawnable unitToSpawn;
                float roll = Random.Range(0, probabilityPool);
                { 
                    unitToSpawn = FindHighestSpawnable(pool, roll);
                    if (unitToSpawn == null)
                    {
                        throw new ArgumentNullException($"Could not find roll random unit: {roll}, {pool}");
                    }
                }

                wave.Add(unitToSpawn);
                points -= unitToSpawn.PointValue;
                pool.RemoveAll(unit => unit.PointValue > points);
            }

            if (wave.Count == 0)
            {
                throw new Exception(
                    $"Could not generate wave from available pool: Points: {points}, Pool Points: {pointPool}");
            }

            return wave;
        }

        protected override List<ISpawnable> Wave()
        {
            List<ISpawnable> wave = BuildWaveFromPoints(NextWavePointValue);
            NextWavePointValue = CalculateNextWavePointValue(NextWavePointValue);
            return wave;
        }
    }
}