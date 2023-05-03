using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ZeroPrep.MineBuddies
{
    [CustomEditor(typeof(ScrollingBackground))]
    public class ScrollingBackgroundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Rebuild Children"))
            {
                ((ScrollingBackground)target).RebuildChildren();
            }


        }
    }
}