using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        public EditorNode node { get; private set; }

        public void Initialize(EditorNode node)
        {
            this.node = node;
            Initialize();
        }

        public void Initialize(TypeData typeData)
        {
            Initialize(new EditorNode(typeData));
        }

        private void Initialize()
        {
            // Use the name as if it would be an id, because this only makes sense
            name = node.id;
            title = node.name;
            SetPosition(new Rect(node.position, Vector2.zero));
            RegisterDragging();
            SetAlwaysExpanded();

            CreateInstanceNameField();
            CreateFields(CodeAnalyzer.GetBlockData(node.typeAssemblyQualifiedName));
            // NOTE May fix this: For any reason if there is just one element the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();

            // NOTE Handle the move event manually, because it does not get triggered in
            // GraphEditorView.graphViewChanged event listeners
            // The current solution is pretty slow and should cause lags when there exist more nodes
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

        private void RegisterDragging()
        {
            var d = new Dragger();
            d.target = this;
        }

        private void SetAlwaysExpanded()
        {
            expanded = true;
            m_CollapseButton.parent.Remove(m_CollapseButton);
        }

        private void CreateInstanceNameField()
        {
            var instanceNameContainer = new NodeViewInstanceNameContainer();
            instanceNameContainer.Initialize(node);
            titleContainer.Add(instanceNameContainer);
            mainContainer.Add(instanceNameContainer.CreatToggle());
        }

        private void CreateFields(BlockData analysisData)
        {
            // Properties
            analysisData.fields.ForEach(data => extensionContainer.Add(new PropertyView(data)));

            // Methods
            analysisData.methods.ForEach(data =>
            {
                inputContainer.Add(PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    typeof(Action),
                    data.name));
            });

            // Triggers
            analysisData.events.ForEach(data =>
            {
                outputContainer.Add(PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Output,
                    Port.Capacity.Multi,
                    typeof(Action),
                    data.name));
            });
        }
    }
}
