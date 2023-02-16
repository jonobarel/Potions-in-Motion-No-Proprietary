using System;

namespace ZeroPrep.MineBuddies
{
    public interface IDisplayable
    {
        public float Value();

        public string ValueString() => $"{Value():00.00}";
        
        
        public event Action<float> ValueChanged;
    }
}