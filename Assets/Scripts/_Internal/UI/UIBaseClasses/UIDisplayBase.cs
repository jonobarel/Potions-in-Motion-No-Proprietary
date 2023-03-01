using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{

    public abstract class UIDisplayBase<TDisplayable, TUIObject> : MonoBehaviour
        where TDisplayable : IDisplayable
    {
        protected IDisplayable Displayable { get; set; }

        protected TUIObject uiObject;
        
        protected abstract void OnValueChange(float nVal);

        public virtual void Awake()
        {
            uiObject = GetComponent<TUIObject>();
            Displayable.ValueChanged += OnValueChange;
        }

        
        public void OnDisable()
        {
            Displayable.ValueChanged -= OnValueChange;
        }
        
        
        
    }
}