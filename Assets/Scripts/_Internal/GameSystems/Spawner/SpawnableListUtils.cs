using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public static class SpawnableListUtils
    {
        private static Dictionary<ISpawnable, float> _probabilityCache = new Dictionary<ISpawnable, float>();
        private static List<ISpawnable> _currentPool;
        public static float ProgressiveProbability(List<ISpawnable> pool, ISpawnable unit)
        {
            if (_currentPool == null || _currentPool != pool)
            {
                _currentPool = pool;
                _probabilityCache.Clear();
            }
            if (_probabilityCache.ContainsKey(unit))
            {
                return _probabilityCache[unit];
            }
            {
                float result = pool.Where(x => pool.IndexOf(x) <= pool.IndexOf(unit)).Sum(u => u.Probability);
                _probabilityCache[unit] = result;
                return result;
            }
        }
        
        public static ISpawnable FindHighestSpawnable(List<ISpawnable> pool, float p)
        {
            return pool.Find(u => ProgressiveProbability(pool, u) >= p);
        }    
        
        public static int CalculatePoints(List<ISpawnable> wave)
        {
            return wave.Sum(u => u.PointValue);
        }

    }
}