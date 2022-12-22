using System;

namespace ZeroPrep.MineBuddies
{
    public interface IDisplayable
    {
        public float Value();
        
        public event Action<float> ValueChanged;
    }
}