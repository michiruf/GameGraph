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

        public static VisualElement GetRoot(this VisualElement element)
        {
            while (element.parent != null)
                element = element.parent;
            return element;
        }
    }
}
