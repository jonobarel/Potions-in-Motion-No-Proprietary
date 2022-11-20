using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;

public class SettingsPrefabMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProjectContext context = GetComponent<ProjectContext>();
        Debug.Log("Listing scriptables");
        foreach (var s in context.ScriptableObjectInstallers)
        {
            Debug.Log($"Scriptable installer: {s.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
