using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        private EditorNode node;
        private EditorParameter parameter;

        private Type nodeType => parameter == null ? node.type : parameter.type;
        private string nodeName => ObjectNames.NicifyVariableName(parameter == null ? node.type.Name : parameter.name);

        private readonly Dragger drag = new Dragger();

        public void Initialize(Type type, Vector2 position, string parameterId)
        {
            Initialize(new EditorNode(type) {position = position, parameterId = parameterId});
        }

        public void Initialize(EditorNode node)
        {
            this.node = node;
            
            // Cancel initialization and remove node if node type does not exist anymore
            if (node.type == null)
            {
                RemoveState();
                RemoveFromHierarchy();
                return;
            }
            
            parameter = node.GetParameter(graph);
            Initialize();
        }

        private void Initialize()
        {
            // Use the name as if it would be an id, because this only makes sense
            name = node.id;
            title = nodeName;
            SetPosition(new Rect(node.position, Vector2.zero));
            drag.target = this;

            if (parameter == null || parameter.isGameGraphType)
                CreateFields(CodeAnalyzer.GetNodeData(nodeType));
            if (parameter != null)
                CreateParameterViews(parameter);

            SetAlwaysExpandedAndRefresh();

            // NOTE Handle the move event manually, because it does not get triggered in
            //      GraphEditorView.graphViewChanged event listeners
            RegisterCallback<MouseMoveEvent>(OnMove);

            // Persist state whenever a value has changed
            RegisterCallback<ControlBase.ControlValueChangeEvent>(evt => PersistState());

            this.GetEventBus().Register(this);
        }

        public void PersistState()
        {
            node.position = GetPosition().position;

            node.propertyValues.Clear();
            // Delegate the collection to controls
            this.Query<ControlBase>()
                .ForEach(controlBase => controlBase.PersistState());

            if (!graph.nodes.Contains(node))
                graph.nodes.Add(node);
        }

        public void RemoveState()
        {
            graph.nodes.Remove(node);
        }

        private void OnMove(MouseMoveEvent evt)
        {
            GetFirstAncestorOfType<GraphEditorView>()
                .graphViewChanged
                .Invoke(new GraphViewChange {movedElements = new List<GraphElement> {this}});
        }

        private void SetAlwaysExpandedAndRefresh()
        {
            expanded = true;
            m_CollapseButton?.parent?.Remove(m_CollapseButton);
            // NOTE May fix this: For any reason if there is just one element the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();
        }

        private void CreateFields(ClassData analysisData)
        {
            // Properties
            analysisData.properties.ForEach(data => extensionContainer.Add(new PropertyView(data, node)));
            analysisData.fields.ForEach(data => extensionContainer.Add(new PropertyView(data, node)));

            // Methods
            analysisData.methods.ForEach(data =>
            {
                inputContainer.Add(PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    typeof(Action),
                    data.info.Name,
                    ObjectNames.NicifyVariableName(data.info.Name)));
            });

            // Triggers
            analysisData.events.ForEach(data =>
            {
                outputContainer.Add(PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Output,
                    Port.Capacity.Multi,
                    typeof(Action),
                    data.info.Name,
                    ObjectNames.NicifyVariableName(data.info.Name)));
            });
        }

        private void CreateParameterViews(EditorParameter parameter)
        {
            // NOTE For parameters maybe put the regular fields into expandable and the instance ports into non-expandable?

            // Flag the node as parameter
            AddToClassList("parameter");

            // Add the output port
            outputContainer.Add(PortFactory.Create(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Multi,
                parameter.type,
                EditorConstants.ParameterPortId,
                EditorConstants.ParameterPortName));
        }

        [UsedImplicitly]
        public void OnEvent(ParameterChangedEvent e)
        {
            if (e.parameter != parameter)
                return;

            // NOTE For immediate easiness just recreate the node
            extensionContainer.Clear();
            inputContainer.Clear();
            outputContainer.Clear();
            UnregisterCallback<MouseMoveEvent>(OnMove);
            Initialize();
        }
    }
}
