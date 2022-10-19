using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;

public class HazardSpawnerTests
{
    class SpawnerTestMonoBehaviour : MonoBehaviour
    {
        
    }
    
    private float _startTime;
    private bool _isSpawned = false;
    private HazardBase _timedHazard;
    private float _minDelay = 0.25f;
    private float _maxDelay = 0.5f;
    
    private void ValidateHazardCreated(HazardBase h)
    {
        Assert.IsNotNull(h);
        _isSpawned = true;
        Assert.IsNotNull(((HazardExternal)h).Type);
        
    }

    [Test]
    public void HazardSpawnsRandomType()
    {
        HazardBase.OnSpawn += ValidateHazardCreated;
        HazardSpawner hs = new HazardSpawner(_minDelay, _maxDelay);
        hs.SpawnRandomTypeHazard();
        Assert.IsTrue(_isSpawned);
        HazardBase.OnSpawn -= ValidateHazardCreated;
    }

    private void ValidateHazardDelay(HazardBase h)
    {
        float currentTime = Time.time;
        Assert.LessOrEqual(currentTime, _startTime+_maxDelay);
        Assert.GreaterOrEqual(currentTime, _startTime + _minDelay);
        
    }
    
    [UnityTest]
    public IEnumerator HazardSpawnDelay()
    {
        GameObject hazardTestGo = new GameObject("HazardTestGO");
        SpawnerTestMonoBehaviour spawnContainer = hazardTestGo.AddComponent<SpawnerTestMonoBehaviour>();
        
        _isSpawned = false;
        HazardBase.OnSpawn += ValidateHazardDelay;
        HazardSpawner hs = new HazardSpawner(_minDelay, _maxDelay);

        _startTime = Time.time;
        hs.StartSpawning((MonoBehaviour)spawnContainer);
        yield return new WaitForSeconds(_maxDelay+0.1f);
        HazardBase.OnSpawn -= ValidateHazardDelay;
    }
    
    /*[UnityTest]
        public IEnumerator HazardExternalTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */


}
