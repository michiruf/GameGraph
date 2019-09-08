using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public class NodeSearchWindowProvider : SearchWindowProviderBase
    {
        public override List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            // TODO Add groups

            // First item in the tree is the title of the window
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent(EditorConstants.NodeSearchWindowHeadline))
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
    }
}
