using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;
using static ZeroPrep.MineBuddies.SpawnableListUtils;
using Assert = UnityEngine.Assertions.Assert;

public class SpawnerUtilsTest
{
    internal class Spawn : ISpawnable
    {
        public int PointValue { get; set; }
        public float Probability { get; set; }
        
        public Spawn(int points, float prob)
        {
            PointValue = points;
            Probability = prob;
        }


    }

    private List<ISpawnable> testList;

    private void InitList()
    {
        testList = new List<ISpawnable>();
        for (int i = 1; i <= 10; i++)
        {
            Spawn goblin = new Spawn(i, 2f/(i+2));
            testList.Add(goblin);
        }
    }


        [Test]
    public void TestProgressiveProbability()
    {
        InitList();
        Dictionary<int, float> values = new Dictionary<int, float>()
        {
            { 0, 0.6666667f },
            { 1, 1.1666667f },
            { 2, 1.5666667f },
            { 3, 1.9000000f },
            { 4, 2.1857143f },
            { 5, 2.4357143f },
            { 6, 2.6579365f },
            { 7, 2.8579365f },
            { 8, 3.0397547f },
            { 9, 3.2064214f }
        };
        
        foreach (var kv in values)
        {
            Assert.AreApproximatelyEqual(kv.Value, ProgressiveProbability(testList, testList[kv.Key]), 0.001f);
        }
    }

    [Test]
    public void TestFindHighestSpawnable()
    {
        InitList();
        Dictionary<int, float> values = new Dictionary<int, float>()
        {
            { 1, 0.6666667f },
            { 2, 1.1666667f },
            { 3, 1.5666667f },
            { 4, 1.9000000f },
            { 5, 2.1857143f },
            { 6, 2.4357143f },
            { 7, 2.6579365f },
            { 8, 2.8579365f },
            { 9, 3.0397547f },
            { 10, 3.2064214f }
        };
       
        foreach (var kv in values)
        {
            Assert.AreEqual(
                (kv.Key),
                FindHighestSpawnable(testList, kv.Value*0.98f).PointValue,
                "Ensuring that get the highest ranked object by points for each progressive probability score"
                );
        }
        
    }
    
    

}
