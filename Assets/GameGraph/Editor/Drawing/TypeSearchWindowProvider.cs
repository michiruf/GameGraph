using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public class TypeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        public Action<Type, Vector2> callback;
        private string title;
        private IEnumerable<TypeData> typeData;
        private Texture2D indent;

        public void Initialize(string title, IEnumerable<TypeData> typeData)
        {
            this.title = title;
            this.typeData = typeData;

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
            var tree = new List<SearchTreeEntry>
            {
                // First item in the tree is the title of the window
                new SearchTreeGroupEntry(new GUIContent(title))
            };
            CreateSearchTree(tree);
            return tree;
        }

        private void CreateSearchTree(List<SearchTreeEntry> tree)
        {
            var groupedTypes = typeData
                .ToLookup(data => data.gameGraphAttribute == null ? "Unity" : data.gameGraphAttribute.group)
                .OrderBy(group => group.Key);

            string previousGroup = null;
            foreach (var lookup in groupedTypes)
            {
                var previousSplit = previousGroup?.Split('/') ?? new string[] { };
                var currentSplit = lookup.Key?.Split('/') ?? new string[] { };
                var level = currentSplit
                    .Where((t, i) => i < previousSplit.Length && string.Equals(previousSplit[i], t))
                    .Count();

                for (var i = level; i < currentSplit.Length; i++)
                {
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(currentSplit[i])) {level = i + 1});
                }

                tree.AddRange(lookup
                    .OrderBy(data => data.type.Name)
                    .Select(data =>
                        new SearchTreeEntry(new GUIContent(data.type.Name, indent))
                        {
                            level = currentSplit.Length + 1,
                            userData = data
                        }));

                previousGroup = lookup.Key;
            }
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            callback?.Invoke((Type) entry.userData, context.screenMousePosition);
            return true;
        }
    }
}
