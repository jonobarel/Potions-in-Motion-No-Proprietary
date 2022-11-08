using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;

public class ModulesTest
{
    
    [UnityTest]
    public IEnumerator ModuleGetEngineByInjectionTest()
    {
        GameObject obj = new GameObject();

        obj.AddComponent<Module>();
        yield return null;
        Module m = obj.GetComponent<Module>();
        Assert.IsNotNull(m);
        Assert.IsNotNull(m.EngineFuel);
    }
}
