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
        
        public List<HazardBase> Hazards => _hazards;
        public HazardManager()
        {
            HazardBase.OnSpawn += HandleNewHazard;
            HazardBase.OnClear += HazardCleared;
            HazardBase.OnExpire += HazardExpired;
        }

        private void HazardExpired(HazardBase h)
        {
            MarkForRemoval(h);
            
        }

        private void HazardCleared(HazardBase h)
        {
            MarkForRemoval(h);
        }

        private void MarkForRemoval(HazardBase h)
        {
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
                h.Advance(delta);
            }

            ClearHazardsToRemove();
        }

        private void ClearHazardsToRemove()
        {
            foreach (HazardBase h in _hazardsToRemove)
            {
                if (!_hazards.Remove(h))
                {
                    throw new System.ArgumentOutOfRangeException($"Attempting to remove a Hazard object that's missing");
                }
            }
            _hazardsToRemove.Clear();
        }
    }
}