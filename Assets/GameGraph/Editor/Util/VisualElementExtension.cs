using System;
using UnityEditor;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class VisualElementExtension
    {
        public static void AddLayout(this VisualElement element, string path)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            Assert.IsNotNull(visualTree, "Asset at " + path + " non existent!");
            visualTree.CloneTree(element);
        }

        public static void AddStylesheet(this VisualElement element, string path)
        {
            var style = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            Assert.IsNotNull(style, "Asset at " + path + " non existent!");
            element.styleSheets.Add(style);
        }

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
