using System;
using System.Collections.Generic;
using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node, IGraphElement
    {
        public RawGameGraph graph { private get; set; }
        public RawNode node { get; private set; }

        public void Initialize(RawNode node)
        {
            this.node = node;
            Initialize();
        }

        public void Initialize(string name)
        {
            Initialize(new RawNode(name));
        }

        private void Initialize()
        {
            title = node.name.PrettifyName();
            SetPosition(new Rect(node.position, Vector2.zero));
            SetAlwaysExpanded();
            RegisterDragging();

            var analysisData = CodeAnalyzer.GetComponentData(node.name);
            CreateFields(analysisData);
            // NOTE May fix this: For any reason if there is just one element the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();

            // NOTE Handle the move event manually, because it does not get triggered in
            // GraphEditorView.graphViewChanged event listeners
            // The current solution is pretty slow and should cause with much nodes lags
            RegisterCallback<MouseMoveEvent>(evt =>
            {
                GetFirstAncestorOfType<GraphEditorView>()
                    .graphViewChanged
                    .Invoke(new GraphViewChange {movedElements = new List<GraphElement> {this}});
            });
        }

        public void PersistState()
        {
            node.position = GetPosition().position;

            if (!graph.nodes.Contains(node))
                graph.nodes.Add(node);
        }

        public void RemoveState()
        {
            graph.nodes.Remove(node);
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
