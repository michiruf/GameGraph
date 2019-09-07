using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class VisualElementDebugExtension
    {
        public static string GetRepresentativeName(this VisualElement element)
        {
            var id = !string.IsNullOrEmpty(element.name) ? $"#{element.name}" : "";

            var classesMethod =
                element.GetType().GetMethod("GetClasses", BindingFlags.Instance | BindingFlags.NonPublic);
            var classesValue = classesMethod?.Invoke(element, new object[] { }) as IEnumerable<string>;
            var classes = classesValue?.Aggregate("", (s, s1) => s + "." + s1);

            return $"{element.GetType().Name} {id} {classes}".Replace("  ", " ");
        }

        public static void PrintHierarchy(this VisualElement element)
        {
            Debug.Log(element.PrintHierarchyInternal());
        }

        private static string PrintHierarchyInternal(this VisualElement element, int depth = 0)
        {
            var childrenString = element.hierarchy.Children()
                .Aggregate("", (current, child) => current + child.PrintHierarchyInternal(depth + 1));
            return new string(' ', depth * 2) + element.GetRepresentativeName() + "\n" + childrenString;
        }
    }
}
