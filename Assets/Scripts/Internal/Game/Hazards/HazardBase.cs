using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public abstract class HazardBase
    {
        private float _health = 1f;

        /// <summary>
        /// Value between 1 and 0.
        /// This is the "health" or "life" of the hazard.
        /// When it reaches 0, the Hazard will call Clear() and the OnClear event will trigger.
        /// </summary>
        public float Health => _health;

        private float _progress = 0f;
        /// <summary>
        /// Value starts at 0 and progresses to 1
        /// When progress reaches 1 the Hazard will call Expire and the OnExpire event will trigger.
        /// </summary>
        public float Progress => _progress;

        public virtual void Advance(float delta)
        {
            _progress += delta;
            OnAdvance?.Invoke();

            if (_progress >= 1f)
            {
                Expire();
            }
        }

        public virtual void Treat(float treatment)
        {
            _health -= treatment;
            OnTreat?.Invoke(this);
            
            if (_health <= 0f)
            {
                Clear();
            }
        }

        protected virtual void Clear()
        {
            OnClear?.Invoke(this);
        }

        protected virtual void Expire()
        {
            OnExpire?.Invoke(this);
        }
        
        /// <summary>
        /// When the Hazard expires, i.e. its timer runs out, this event is invoked.
        /// This happens when an external hazard hits the carriage, when an internal hazard triggers damage, etc.
        /// </summary>
        public static event Action<HazardBase> OnExpire;
        /// <summary>
        /// This event is called when the hazard is cleared by the players. If this is called, Expire will not happen.
        /// </summary>
        public static event Action<HazardBase> OnClear;
        public static event Action<HazardBase> OnTreat;

        /// <summary>
        /// Register this event for updating UI-related fields.
        /// </summary>
        public static event Action OnAdvance; 
        


    }
}