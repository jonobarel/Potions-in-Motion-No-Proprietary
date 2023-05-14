using System;

namespace ZeroPrep.UI
{
    public interface IDisplayable
    {
        public float Value();

        public string ValueString() => $"{Value():00.00}";
        
        
        public event Action<float> ValueChanged;
    }
}