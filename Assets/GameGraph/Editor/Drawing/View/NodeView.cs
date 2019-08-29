using System;
using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node, INode
    {
        public RawGameGraph graph { get; set; }
        public RawNode node { get; set; }

        public void Initialize()
        {
            title = node.name.PrettifyName();
            SetPosition(new Rect(node.position, Vector2.zero));
            SetAlwaysExpanded();
            RegisterDragging();

            var analysisData = CodeAnalyzer.GetComponentData(node.name);
            CreateFields(analysisData);
            // NOTE May fix this
            // For any reason if there is just one element, the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();
        }

        public void Save()
        {
            node.position = GetPosition().position;

            if (!graph.nodes.Contains(node))
                graph.nodes.Add(node);
        }

        private void SetAlwaysExpanded()
        {
            expanded = true;
            var collapseButton = this.FindElementByName<VisualElement>("collapse-button");
            collapseButton.GetFirstAncestorOfType<VisualElement>().Remove(collapseButton);
        }

        private void RegisterDragging()
        {
            var d = new Dragger();
            d.target = this;
        }

        private void CreateFields(ComponentData analysisData)
        {
            // Properties
            analysisData.properties.ForEach(data => extensionContainer.Add(new PropertyView(data)));

            // Methods
            analysisData.methods.ForEach(data =>
            {
                var port = Port.Create<EdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                    typeof(Action));
                port.portName = data.name.PrettifyName();
                inputContainer.Add(port);
            });

            // Triggers
            analysisData.triggers.ForEach(data =>
            {
                var port = Port.Create<EdgeView>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,
                    typeof(Action));
                port.portName = data.name.PrettifyName();
                outputContainer.Add(port);
            });
        }
    }
}
