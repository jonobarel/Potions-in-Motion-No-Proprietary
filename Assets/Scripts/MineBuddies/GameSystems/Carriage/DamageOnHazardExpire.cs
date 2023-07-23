using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using Zenject;
using UnityEngine;
using ZeroPrep.MineBuddies;

[RequireComponent(typeof(Health))]
public class DamageOnHazardExpire : MonoBehaviour
{
    private bool _isInitialized = false;
    private Health _health;

    private GameSettings _gameSettings;
    private int _startingHealth;
    
    void Start()
    {
        _health = GetComponent<Health>();
        HazardBase.Expire += OnHazardExpire;
        if (!_isInitialized)
        {
            throw new ArgumentException("Awake called before Init?");
        }
        else
        {
            _health.SetHealth(_startingHealth, gameObject);
        }
        
    }

    [Inject]
    private void Init(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
        _startingHealth = gameSettings.VehicleStartingHealth;
        _isInitialized = true;
    }
    
    private void OnHazardExpire(HazardBase h)
    {
        _health.Damage((int)(h.DamageFactor*_gameSettings.BaseDamageOnHazardExpire), gameObject, _gameSettings.DamageFlickerDuration, _gameSettings.InvulnerabilityDuration, Vector3.zero);
    }

    private void OnDestroy()
    {
        HazardBase.Expire -= OnHazardExpire;
    }
}
