using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace ZeroPrep.MineBuddies
{
    [CustomEditor(typeof(ModulePlacement))]

    public class ModulePlacementEditor : ObjectContainerEditor<Transform>
    {

    }
}
#endif