using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineHealthTest : MonoBehaviour
{
    // Start is called before the first frame update
    public MoreMountains.CorgiEngine.Health health;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthUp()
    {
        Debug.Log("Adding health to engine");
        if (health.CurrentHealth <= 0)
            health.Revive();
        health.Damage(-10, gameObject, 0.5f, 0f, Vector3.zero);
    }

    public void HealthDown()
    {
        Debug.Log("removing health from engine");
        
        health.Damage(10, gameObject, 0.5f, 0f, Vector3.zero);
    }
}
