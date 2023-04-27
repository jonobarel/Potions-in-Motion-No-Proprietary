using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardManagerDebugMonitor : MonoBehaviour
    {
        [SerializeField] private List<int> _activeHazards = new List<int>();
        [SerializeField] private List<int> _expiredHazards = new List<int>();
        [SerializeField] private List<int> _clearedHazards = new List<int>();
        [SerializeField] private List<int> _removedHazards = new List<int>();
        [SerializeField] private List<int> _spawnedHazards = new List<int>();

        private void OnEnable()
        {
            HazardBase.Spawn += OnSpawn;
            HazardBase.Clear += OnClear;
            HazardBase.Expire += OnExpire;
        }

        private void OnDestroy()
        {
            HazardBase.Spawn -= OnSpawn;
            HazardBase.Clear -= OnClear;
            HazardBase.Expire -= OnExpire;
        }

        private void OnSpawn(HazardBase h)
        {
            _spawnedHazards.Add(h.ID);
            _activeHazards.Add(h.ID);
        }

        private void OnClear(HazardBase h)
        {
            _activeHazards.Remove(h.ID);
            _clearedHazards.Add(h.ID);
            _removedHazards.Add(h.ID);
        }

        private void OnExpire(HazardBase h)
        {
            _activeHazards.Remove(h.ID);
            _expiredHazards.Add(h.ID);
            _removedHazards.Add(h.ID);
        }
    }
}