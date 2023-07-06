using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.MineBuddies;

public class ResourceLoad : MonoBehaviour
{
    [SerializeField] private string resourcePath;
    [SerializeField]
    GameSettingsInstaller[] resources;
    
    void Start()
    {
        resources = Resources.FindObjectsOfTypeAll<GameSettingsInstaller>();
        Debug.Log(resources);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
