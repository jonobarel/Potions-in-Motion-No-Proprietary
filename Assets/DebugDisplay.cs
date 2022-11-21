using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using ZeroPrep.MineBuddies;

public class DebugDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text speed;
    public TMP_Text fuel;
    public TMP_Text targetSpeed;

    private EngineSpeed _engineSpeed;
    private EngineFuel _engineFuel;

    [Inject]
    private void Init(EngineSpeed speed, EngineFuel fuel)
    {
        _engineSpeed = speed;
        _engineFuel = fuel;
        
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = $"Speed: {_engineSpeed.CurrentSpeed()}";
        fuel.text = $"Fuel: {_engineFuel.FuelLevel}";
        targetSpeed.text = $"Target speed: {_engineSpeed.TargetSpeed}";

    }
}
