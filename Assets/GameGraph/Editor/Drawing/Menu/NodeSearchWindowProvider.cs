using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private GraphEditorView graphView;
        private GameGraphWindow window;
        private Action<Type, Vector2?> callback;

        private Texture2D indent;

        public void Initialize(GraphEditorView graphView, GameGraphWindow window, Action<Type, Vector2?> callback)
        {
            this.graphView = graphView;
            this.window = window;
            this.callback = callback;

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

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            // TODO Add groups

            // First item in the tree is the title of the window
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node"))
            };

            CodeAnalyzer.GetNodeTypes()
                .ToList()
                .ForEach(type =>
                {
                    var guiContent = new GUIContent(type.Name, indent);
                    var entry = new SearchTreeEntry(guiContent)
                    {
                        level = 1,
                        userData = type
                    };
                    tree.Add(entry);
                });

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            // Get position
            var windowRoot = window.rootVisualElement;
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
                context.screenMousePosition - window.position.position);
            var graphMousePosition = graphView.contentViewContainer.WorldToLocal(windowMousePosition);

            callback?.Invoke((Type) entry.userData, graphMousePosition);
            return true;
        }
    }
}
