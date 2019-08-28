using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class VisualElementDebugExtension
    {
        public static string GetRepresentativeName(this VisualElement element)
        {
            return $"{element.GetType().Name} - {element.name}";
        }

        public static void PrintHierarchy(this VisualElement element)
        {
            Debug.Log(element.PrintHierarchyInternal());
        }

        private static string PrintHierarchyInternal(this VisualElement element, int depth = 0)
        {
            var childrenString = element.Children()
                .Aggregate("", (current, child) => current + child.PrintHierarchyInternal(depth + 1));
            return new string(' ', depth * 2) + element.GetRepresentativeName() + "\n" + childrenString;
        }
    }
}
