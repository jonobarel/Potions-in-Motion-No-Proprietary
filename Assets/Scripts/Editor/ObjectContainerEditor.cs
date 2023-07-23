#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class ObjectContainerEditor<T> : Editor
    where T : Component
    {
        public override void OnInspectorGUI()
        {
            ObjectContainer<T> container = (ObjectContainer<T>)target;
            serializedObject.Update ();
            DrawDefaultInspector();
            if (GUILayout.Button("Generate "+ container.Prefab.name + "s"))
            {
                container.PlaceObjects();
            }

            if (GUILayout.Button("Clear "+ container.Prefab.name + "s"))
            {
               container.ClearContents();
            }

        }
    }
}

#endif