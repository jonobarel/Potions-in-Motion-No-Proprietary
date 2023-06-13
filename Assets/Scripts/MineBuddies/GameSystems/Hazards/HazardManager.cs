using System;
using System.Collections.Generic;
using System.Linq;
using ZeroPrep.MineBuddies;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// <c>HazardManager</c> is meant to handle the moment-to-moment behaviour of the hazards:
    /// It should contain a list of hazards,
    /// a spawning mechanism,
    /// a mechanism for registering events to hazards
    /// 
    /// </summary>
    public class HazardManager
    {
        private List<HazardBase> _hazards = new List<HazardBase>();
        private List<HazardBase> _hazardsToRemove = new List<HazardBase>();

        public event Action<HazardBase> HazardExpired;
        public event Action<HazardBase> HazardCleared;
        
        public List<HazardBase> Hazards => _hazards;
        public HazardManager()
        {
            HazardBase.Spawn += OnSpawn;
            HazardBase.Clear += OnClear;
            HazardBase.Expire += OnExpire;
        }

        ~HazardManager()
        {
            HazardBase.Spawn -= OnSpawn;
            HazardBase.Clear -= OnClear;
            HazardBase.Expire -= OnExpire;
        }
        private void OnSpawn(HazardBase h)
        {
            HandleNewHazard(h);
        }
        private void OnExpire(HazardBase h)
        {
            HazardExpired?.Invoke(h);
            MarkForRemoval(h);
        }

        private void OnClear(HazardBase h)
        {
            HazardCleared?.Invoke(h);
            MarkForRemoval(h);
            
        }

        private void MarkForRemoval(HazardBase h)
        {
            _hazards.Remove(h);
            _hazardsToRemove.Add(h);
        }


        private void HandleNewHazard(HazardBase h)
        {
            _hazards.Add(h);
        }

        public HazardBase GetClosestHazardOfType(Managers.HazardType type)
        {
            var results = _hazards.FindAll(h => h.Type == type);
            if (results.Count > 0)
            {
                return results.First();
            }
            else
            {
                return null;
            }

        }

        public void Update(float delta)
        {
            foreach (HazardBase h in _hazards)
            {
                h.AdvanceAction(delta);
            }

            ClearHazardsToRemove();
        }

        private void ClearHazardsToRemove()
        {
            _hazardsToRemove.Clear();
        }
    }
}