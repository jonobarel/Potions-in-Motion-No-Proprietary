using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private Singleton instance;
    public Singleton Instance
    {
        get
        {
            if (instance == null)
                instance = GetComponent<Singleton>();
            return instance;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
