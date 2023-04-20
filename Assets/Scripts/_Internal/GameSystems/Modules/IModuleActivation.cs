using UnityEngine.Events;

namespace ZeroPrep.MineBuddies
{
    public interface IModuleActivation
    {
        public HazardManagerGO.InteractionType InteractionType { get; }

        public bool Activable { get; set; }
    }
}