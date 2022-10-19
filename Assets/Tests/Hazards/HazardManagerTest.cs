using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;
using type = ZeroPrep.MineBuddies.Managers.HazardType;

public class HazardManagerTest
{
    // A Test behaves as an ordinary method
    
    
    [Test]
    public void DetectHazardCreated()
    {
        HazardManager hazardManager = new HazardManager();
        Assert.AreEqual(hazardManager.Hazards.Count, 0);
        HazardExternal hazard1 = new HazardExternal(1f, type.A);
        Assert.AreEqual(hazardManager.Hazards.Count, 1);
        HazardExternal hazard2 = new HazardExternal(1f, type.B);
        Assert.AreEqual(hazardManager.Hazards.Count, 2);

    }

    [Test]
    public void HazardProgression()
    {
        HazardManager hazardManager = new HazardManager();
        HazardExternal hazard1 = new HazardExternal(0.5f, type.A);
        hazardManager.Update(1f);
        Assert.AreEqual(0.5f, hazard1.Progress);
    }

    [Test]
    public void HazardExpiresTest()
    {
        HazardManager hazardManager = new HazardManager();
        HazardExternal hazard1 = new HazardExternal(1f, type.A);
        HazardExternal hazard2 = new HazardExternal(0.5f, type.A);
        HazardExternal hazard3 = new HazardExternal(0.25f, type.A);
        hazardManager.Update(1f);
        Assert.AreEqual(2,hazardManager.Hazards.Count);
        hazardManager.Update(1f);
        Assert.AreEqual(1, hazardManager.Hazards.Count);
        hazardManager.Update(10f);
        Assert.AreEqual(0, hazardManager.Hazards.Count);
    }

    [Test]
    public void HazardTreatTest()
    {
        HazardManager hazardManager = new HazardManager();
        HazardExternal hazard1 = new HazardExternal(1f, type.A);
        hazard1.Treat(1f);
        hazardManager.Update(0.1f);
        Assert.AreEqual(0, hazardManager.Hazards.Count);
    }

    [Test]
    public void GetHazardsOfType()
    {
        HazardManager hazardManager = new HazardManager();
        type[] types =
        {
            type.A,
            type.A,
            type.B,
            type.B,
            type.B,
            type.E
        };

        HazardExternal closestA = null;
        HazardExternal closestB = null;
        HazardExternal closestE = null;
        
        foreach (var t in types)
        {
            HazardExternal h = new HazardExternal(1f, t);
            if (closestA == null && t == type.A)
            {
                closestA = h;
            }
            else if (closestB == null && t == type.B)
            {
                closestB = h;
            }
            else if (closestE == null && t == type.E)
            {
                closestE = h;
            }
            hazardManager.Update(0.1f);
        }

        Assert.AreEqual(hazardManager.GetClosestHazardOfType(type.A), closestA);
        Assert.AreEqual(hazardManager.GetClosestHazardOfType(type.B), closestB);
        Assert.AreEqual(hazardManager.GetClosestHazardOfType(type.E), closestE);
        Assert.IsNull(hazardManager.GetClosestHazardOfType(type.C));
    }

}
