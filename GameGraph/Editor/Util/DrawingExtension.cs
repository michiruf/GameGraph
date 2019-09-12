using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class DrawingExtension
    {
        #region Layout & Style Helper

        public static void AddLayout(this VisualElement element, string path)
        {
            var visualTree = Resources.Load<VisualTreeAsset>(path);;
            Assert.IsNotNull(visualTree, "Asset at " + path + " non existent!");
            visualTree.CloneTree(element);
        }

        public static void AddStylesheet(this VisualElement element, string path)
        {
            var style = Resources.Load<StyleSheet>(path);
            Assert.IsNotNull(style, "Asset at " + path + " non existent!");
            element.styleSheets.Add(style);
        }

        public static Vector2 GetScreenPosition(this VisualElement container)
        {
            return container.GetWindow().position.position + container.LocalToWorld(Vector2.zero);
        }

        public static Vector2 GetContainerRelativePosition(this VisualElement container, Vector2 positionOnScreen)
        {
            // This is in fact a bit complicated, but copied from the shader graph because
            // the GraphView itself is scrollable. And because this works, why not.
            var window = container.GetWindow();
            var windowRoot = window.rootVisualElement;
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
                positionOnScreen - window.position.position);
            return container.WorldToLocal(windowMousePosition);
        }

        #endregion


        #region Lookups

        public static T QCached<T>(this VisualElement element, string name = null, string className = null)
            where T : VisualElement
        {
            var id = $"{name}-{className}";
            if (!element.HasUserData<T>(id))
                element.AddUserData(element.Q<T>(name, className), id);
            return element.GetUserData<T>(id);
        }

        public static VisualElement GetRoot(this VisualElement element)
        {
            // Go only until rootVisualContainer because when the windows is finalized after
            // construction, there is a container more (a panel) and that panel cannot
            // be addressed in the constructor
            while (element.parent != null && !string.Equals(element.viewDataKey, "rootVisualContainer"))
                element = element.parent;
            return element;
        }

        #endregion


        #region Window Receiving

        public static void MakeWindowReceivableByChildren(this EditorWindow window)
        {
            window.rootVisualElement.AddUserData(window);
        }

        public static EditorWindow GetWindow(this VisualElement element)
        {
            return element.GetWindow<EditorWindow>();
        }

        public static T GetWindow<T>(this VisualElement element) where T : EditorWindow
        {
            return element.GetRoot().GetUserData<EditorWindow>() as T ??
                   throw new Exception("Your window must call this.MakeWindowReceivableByChildren() before!");
        }

        #endregion


        #region Event Bus Receiving

        public static void AddEventBus(this EditorWindow window)
        {
            window.rootVisualElement.AddUserData(new EventBus());
        }

        public static void RemoveEventBus(this EditorWindow window)
        {
            window.rootVisualElement.RemoveUserData<EventBus>();
        }

        public static EventBus GetEventBus(this VisualElement element)
        {
            if (!element.HasUserData<EventBus>())
                element.AddUserData(element.GetRoot().GetUserData<EventBus>());
            return element.GetUserData<EventBus>();
        }

        #endregion

        
        #region Prettify Name

        public static string PrettifyName(this string input)
        {
            return Regex.Replace(
                    Regex.Replace(
                        input.FirstLetterToUpperCase(),
                        @"(\P{Ll})(\P{Ll}\p{Ll})",
                        "$1 $2"
                    ),
                    @"(\p{Ll})(\P{Ll})",
                    "$1 $2"
                )
                .Trim()
                .FirstLetterToUpperCase();
        }

        public static string FirstLetterToUpperCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        #endregion
    }
}
