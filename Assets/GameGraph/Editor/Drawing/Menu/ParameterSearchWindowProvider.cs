using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public class ParameterSearchWindowProvider : SearchWindowProviderBase
    {
        public override List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            // First item in the tree is the title of the window
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent(EditorConstants.ParameterSearchWindowHeadline))
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

            CodeAnalyzer.GetNonNodeTypes()
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
