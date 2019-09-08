using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public abstract class SearchWindowProviderBase : ScriptableObject, ISearchWindowProvider
    {
        public Action<Type, Vector2> callback;
        protected Texture2D indent;

        public void Initialize()
        {
            // Transparent icon to trick search window into indenting items
            indent = new Texture2D(1, 1);
            indent.SetPixel(0, 0, new Color(0, 0, 0, 0));
            indent.Apply();
        }

        void OnDestroy()
        {
            if (indent != null)
                DestroyImmediate(indent);
        }

        public abstract List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context);

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
//            // Get position
//            var window = targetContainer.GetWindow();
//            var windowRoot = window.rootVisualElement;
//            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
//                context.screenMousePosition - window.position.position);
//            var graphMousePosition = targetContainer.WorldToLocal(windowMousePosition);

            callback?.Invoke((Type) entry.userData, context.screenMousePosition);
            return true;
        }
    }
}
