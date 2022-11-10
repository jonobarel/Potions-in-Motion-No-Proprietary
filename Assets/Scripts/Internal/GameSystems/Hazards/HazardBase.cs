using System;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public abstract class HazardBase
    {
        
        private Managers.HazardType _type;
        public Managers.HazardType Type => _type;
        
        /// <summary>
        /// Value between 1 and 0.
        /// This is the "health" or "life" of the hazard.
        /// When it reaches 0, the Hazard will call Clear() and the OnClear event will trigger.
        /// </summary>
        public float Health => _health;
        private float _health;

        private float _progress = 0f;
        /// <summary>
        /// Value starts at 0 and progresses to 1
        /// When progress reaches 1 the Hazard will call Expire and the OnExpire event will trigger.
        /// </summary>
        public float Progress => _progress;

        public virtual void AdvanceAction(float delta)
        {
            _progress += delta;
            Advance?.Invoke(this);

            if (_progress >= 1f)
            {
                ExpireAction();
            }
        }

        protected HazardBase(Managers.HazardType type, float startingHealth)
        {
            _health = startingHealth;
            _type = type;
            Spawn?.Invoke(this);
        }
        public virtual void TreatAction(float treatment)
        {
            _health -= treatment;
            Treat?.Invoke(this);
            
            if (_health <= 0f)
            {
                ClearAction();
            }
        }

        protected virtual void ClearAction()
        {
            Clear?.Invoke(this);
        }

        protected virtual void ExpireAction()
        {
            Expire?.Invoke(this);
        }
        
        
        /// <summary>
        /// When the Hazard expires, i.e. its timer runs out, this event is invoked.
        /// This happens when an external hazard hits the carriage, when an internal hazard triggers damage, etc.
        /// </summary>
        public static event Action<HazardBase> Expire;
        /// <summary>
        /// This event is called when the hazard is cleared by the players. If this is called, Expire will not happen.
        /// </summary>
        public static event Action<HazardBase> Clear;
        public static event Action<HazardBase> Treat;

        /// <summary>
        /// Register this event for updating UI-related fields.
        /// </summary>
        public static event Action<HazardBase> Advance;

        public static event Action<HazardBase> Spawn;


    }
}