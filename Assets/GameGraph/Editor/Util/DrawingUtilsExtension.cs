using System;
using UnityEditor;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace GameGraph.Editor.Util
{
    public static class DrawingUtilsExtension
    {
        public static void AddLayout(this VisualElement root, String path)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            Assert.IsNotNull(visualTree, "Asset at " + path + " non existent!");
            visualTree.CloneTree(root);
        }

        public static void AddStylesheet(this VisualElement element, String path)
        {
            var style = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            Assert.IsNotNull(style, "Asset at " + path + " non existent!");
            element.styleSheets.Add(style);
        }
    }
}
