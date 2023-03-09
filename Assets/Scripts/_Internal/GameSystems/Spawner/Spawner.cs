using System.Collections.Generic;
using System.Linq;

namespace ZeroPrep.MineBuddies
{

    public abstract class Spawner
    {
        public int StartingWaveSize { get; set; }

        public int CurrentWaveIndex
        {
            get => AllWaves.Count;
        }

        public float TotalProbabilities => Units.Sum(u => u.Probability);
        private int TotalPoints => Units.Sum(u => u.PointValue);
        
        public int NextWaveSize { get; protected set; }

        public List<ISpawnable> Units { get; protected set; }
        public List<List<ISpawnable>> AllWaves { get; protected set; }
        
        public Spawner(List<ISpawnable> units, int startingWaveSize)
        {
            AllWaves = new List<List<ISpawnable>>();

            Units = units.ToList();
            Units.Sort((s1, s2 )=>  s2.Probability.CompareTo(s1.Probability));
            StartingWaveSize = startingWaveSize;
        }

        public List<ISpawnable> SpawnWave()
        {
            List<ISpawnable> wave = Wave();
            AllWaves.Add(wave);
            return wave;
        }

        protected abstract List<ISpawnable> Wave();

    }
}