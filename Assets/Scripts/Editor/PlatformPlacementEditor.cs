using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR

namespace ZeroPrep.MineBuddies
{
    [CustomEditor(typeof(PlatformPlacement))]
    public class PlatformPlacementEditor : ObjectContainerEditor<Transform>
    {
       

        public void OnSceneGUI()
        {
            Bounds levelBounds = ((PlatformPlacement)target).ScaledBounds;
            
            Handles.color = Color.blue;
            Handles.DrawWireCube(levelBounds.center, levelBounds.size);
        }
    }
}

#endif