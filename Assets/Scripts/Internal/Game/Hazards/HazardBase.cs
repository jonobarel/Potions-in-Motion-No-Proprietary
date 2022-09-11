using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public abstract class HazardBase
    {
        private float _lifetime;

        private float _health;

        public abstract void Advance(float delta);

        public abstract void Treat();
        protected abstract void Clear();
        protected abstract void Expire();
        
        /// <summary>
        /// When the Hazard expires, i.e. its timer runs out, this event is invoked.
        /// This happens when an external hazard hits the carriage, when an internal hazard triggers damage, etc.
        /// </summary>
        public static event Action OnExpire;
        /// <summary>
        /// This event is called when the hazard is cleared by the players. If this is called, Expire will not happen.
        /// </summary>
        public static event Action OnClear;
        public static event Action OnTreat;

        /// <summary>
        /// Register this event for updating UI-related fields.
        /// </summary>
        public static event Action OnAdvance; 
        


    }
}