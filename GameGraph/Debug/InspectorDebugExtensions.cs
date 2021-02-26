using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    public static class InspectorDebugExtensions
    {
        public static void PrintHierarchy(this SerializedObject element, bool onlyVisible = true)
        {
            element.GetIterator().Copy().PrintHierarchy(onlyVisible);
        }

        public static void PrintHierarchy(this SerializedProperty element, bool onlyVisible = true)
        {
            while (onlyVisible ? element.NextVisible(true) : element.Next(true))
            {
                Debug.Log(element.name);
            }
        }
    }
}
