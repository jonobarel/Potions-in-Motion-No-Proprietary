using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;
using Assert = UnityEngine.Assertions.Assert;
using static ZeroPrep.MineBuddies.SpawnableListUtils;

public class ProbabilitySpawnerTest
{
    internal class Goblin : ISpawnable
    {
        public int PointValue { get; set; }
        public float Probability { get; set; }

        public Goblin(int points, float prob)
        {
            PointValue = points;
            Probability = prob;
        }
    }

    private const int NUMBER_OF_WAVES=1000;
    private List<ISpawnable> InitUnits(int maxGoblins = 10)
    {
        List<ISpawnable> units = new List<ISpawnable>();
        for (int i = 1; i <= maxGoblins; i++)
        {
            Goblin goblin = new Goblin(i, 2f/(i+2));
            units.Add(goblin);
        }

        return units;
    }
    
    private ProbabilitySpawner GetNewSpawner(int StartingSize, int StartingPoints, int maxGoblins = 10)
    {
        return new ProbabilitySpawner(InitUnits(maxGoblins), StartingSize,StartingPoints);
    }

    private delegate List<ISpawnable> SpawnMethod();

    private double TimeMeasure(out List<ISpawnable> list , SpawnMethod method)
    {
        DateTime before = DateTime.Now;
        list = method();
        DateTime after = DateTime.Now;
        return (after - before).TotalSeconds;

    }
    
    /// <summary>
    /// Check that all waves generated are less than or equal to the expected point value
    /// </summary>
    [Test]
    public void TestWavesGeneratedWithinPointValue()
    {
        ProbabilitySpawner goblinArmy = GetNewSpawner(1, 4);

        for (int i = 0; i <= NUMBER_OF_WAVES; i++)
        {
            int nextWavePoints = goblinArmy.NextWavePointValue;
            List<ISpawnable> wave = goblinArmy.SpawnWave();
            Assert.IsTrue(CalculatePoints(wave) <= nextWavePoints, $"WavePoints: {CalculatePoints(wave)}, Expected <= {nextWavePoints}");
        }
    }

    /// <summary>
    /// Generates a single wave and validates that the wave is generated correctly with no errors.
    /// </summary>
    [Test]
    public void TestSingleWave()
    {
        List<ISpawnable> wave = GetNewSpawner(1, 10,4).SpawnWave();
        Assert.IsTrue(CalculatePoints(wave) <= 10);
    }

    /// <summary>
    /// Check that waves aren't generated for illegal point value requests
    /// </summary>
    [Test]
    public void TestWavePointErrors()
    {
        List<ISpawnable> goblinUnits = InitUnits();
        
        for (int i = 1; i <= NUMBER_OF_WAVES; i++)
        {
            List<ISpawnable> subset = goblinUnits.Where(goblin => goblin.PointValue > i).ToList();
            ProbabilitySpawner goblinArmy = new ProbabilitySpawner(subset, 1, i);
            bool exceptionThrown = false;
            try
            {
                goblinArmy.SpawnWave();
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }
    }

    /// <summary>
    /// This tests that over time, the frequency of generating a particular unit matches
    /// their relative frequency within the list.
    /// </summary>
    [Test]
    public void TestUnitProbability()
    {
        ProbabilitySpawner goblinArmy = GetNewSpawner(1, 3);
        int totalUnitsManualCount = 0;
        for (int i = 0; i < NUMBER_OF_WAVES; i++)
        {
            int nextWavePoints = goblinArmy.NextWavePointValue;
            List<ISpawnable> wave = goblinArmy.SpawnWave();
            int waveSize=wave.Count;
            //Debug.Log($"Spawning wave {i}, number of units: {waveSize}, Expected points value: {nextWavePoints}, Actual points value: {CalculatePoints(wave)}");
            totalUnitsManualCount += waveSize;
        }

        
        int totalUnits = goblinArmy.AllWaves.Sum(wave => wave.Count);
        
        Assert.AreEqual(totalUnits, totalUnitsManualCount, "counting units by wave vs. summing AllWaves");
        
        foreach (ISpawnable g in goblinArmy.Units)
        {
            int occurrences = goblinArmy.AllWaves.Sum(wave => wave.Count(u => u == g));
            float expectedFrequency = g.Probability / goblinArmy.TotalProbabilities;
            float actualFrequency = (float)occurrences / totalUnits;
            Assert.AreApproximatelyEqual(expectedFrequency, 
                actualFrequency,
                0.1f,
                $"Actual frequency vs. expected frequency. Unit: {g.PointValue}, Occurrences: {occurrences}, total units: {totalUnits}");
        }
        
        
    }
  
    
}
