using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public class ReferenceSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private GraphEditorView graphView;
        private GameGraphWindow window;
        private Action<TypeData> callback;

        private Texture2D indent;

        public void Initialize(Action<TypeData> callback)
        {
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
            // First item in the tree is the title of the window
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Select Type"))
            };

            CodeAnalyzer.GetNodeTypes()
                .ToList()
                .ForEach(typeData =>
                {
                    var guiContent = new GUIContent(typeData.name, indent);
                    var entry = new SearchTreeEntry(guiContent)
                    {
                        level = 1,
                        userData = typeData
                    };
                    tree.Add(entry);
                });

            CodeAnalyzer.GetNonNodeTypes()
                .ToList()
                .ForEach(typeData =>
                {
                    var guiContent = new GUIContent(typeData.name, indent);
                    var entry = new SearchTreeEntry(guiContent)
                    {
                        level = 1,
                        userData = typeData
                    };
                    tree.Add(entry);
                });

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            callback?.Invoke((TypeData) entry.userData);
            return true;
        }
    }
}
