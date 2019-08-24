using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class VisualElementExtension
    {
        [Obsolete("Use Queries for VisualElements")]
        public static T FindElementByName<T>(this VisualElement element, string name) where T : VisualElement
        {
            var result = FindElementByNameInternal<T>(element, name);
            if (result == null)
                throw new ArgumentException("Element with name " + name + " could not be found");
            return result;
        }

        private static T FindElementByNameInternal<T>(this VisualElement element, string name) where T : VisualElement
        {
            if (name.Equals(element.name))
                return element as T;

            foreach (var child in element.hierarchy.Children())
            {
                var result = FindElementByNameInternal<T>(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
