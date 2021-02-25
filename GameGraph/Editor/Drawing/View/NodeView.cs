using System;
using System.Linq;
using System.Reflection;
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

        public void Initialize(Type type, Vector2 position, string parameterId)
        {
            Initialize(new EditorNode(type) {position = position, parameterId = parameterId});
        }

        public void Initialize(EditorNode node)
        {
            this.node = node;
            Initialize();
        }

        private void Initialize()
        {
            // Receive parameter if available or cancel
            parameter = node.GetParameter(graph);
            if (node.isParameter && parameter == null)
            {
                var eventBus = this.GetEventBus();
                RemoveState();
                RemoveFromHierarchy();
                eventBus.Dispatch(new GraphChangedEvent());
                return;
            }

            // Cancel initialization and remove node if node type does not exist anymore
            if (nodeType == null)
            {
                var eventBus = this.GetEventBus();
                RemoveState();
                RemoveFromHierarchy();
                eventBus.Dispatch(new GraphChangedEvent());
                return;
            }

            // Use the name as if it would be an id, because this only makes sense
            name = node.id;
            title = nodeName;
            SetPosition(new Rect(node.position, Vector2.zero));

            if (parameter == null || parameter.isGameGraphType)
                CreateFields(CodeAnalyzer.GetNodeData(nodeType));
            if (parameter != null)
                CreateParameterViews(parameter);

            SetAlwaysExpandedAndRefresh();
            RegisterDoubleClickOpensAsset();

            // NOTE Potentially a memory leak
            this.GetEventBus().Register(this);
        }

        public void PersistState()
        {
            // Update the position if it is not the initial one
            var position = GetPosition().position;
            if (!Vector2.zero.Equals(position))
                node.position = position;

            node.propertyValues.Clear();
            // Delegate the collection to controls
            this.Query<Control>()
                .ForEach(controlBase => controlBase.PersistState());

            if (!graph.nodes.Contains(node))
                graph.nodes.Add(node);
        }

        public void RemoveState()
        {
            graph.nodes.Remove(node);
        }

        private void SetAlwaysExpandedAndRefresh()
        {
            expanded = true;
            m_CollapseButton?.parent?.Remove(m_CollapseButton);
            // NOTE May fix this: For any reason if there is just one element the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();
        }

        private void RegisterDoubleClickOpensAsset()
        {
            RegisterCallback<MouseDownEvent>(evt =>
            {
                if (evt.clickCount != 2)
                    return;

                var assetTuple = AssetDatabase.GetAllAssetPaths()
                    .Where(s => s.EndsWith(".cs"))
                    .Select(s => AssetDatabase.LoadMainAssetAtPath(s) as MonoScript)
                    .Where(script => script != null)
                    .Select(script => Tuple.Create(script, script.GetClass()))
                    .FirstOrDefault(tuple => tuple.Item2 == nodeType);

                if (assetTuple != null)
                    AssetDatabase.OpenAsset(assetTuple.Item1);
            });
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
            Initialize();
        }

        [UsedImplicitly]
        public void OnEvent(ControlValueChangedEvent e)
        {
            // Flag dirty when a value has changed
            node.isDirty = true;

            // Persist state whenever a value has changed
            PersistState();
        }
    }
}
