using UnityEditor;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            int index = property.intValue;
            if (index > 31 || index < 0)
            {
                Debug.Log("Invalid layer index. Setting to 0");
                index = 0;
            }
            property.intValue = EditorGUI.LayerField(position, label, index);
            
            EditorGUI.EndProperty();
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
    
    public class LayerAttribute : PropertyAttribute
    {
        
    }
}