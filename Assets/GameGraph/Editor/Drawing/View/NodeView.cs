using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public GameGraph graph { get; set; }
        private Node node;

        public void Initialize(Node node)
        {
            this.node = node;

            title = node.name.PrettifyName();
            SetPosition(new Rect(node.position, Vector2.zero));
            SetAlwaysExpanded();
            RegisterDragAndDrop();
            RegisterPositionCallback();

            var analysisData = CodeAnalyzer.GetComponentData(node.name);
            CreateFields(analysisData);
            // NOTE May fix this
            // For any reason if there is just one element, the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();
        }

        private void SetAlwaysExpanded()
        {
            expanded = true;
            var collapseButton = this.FindElementByName<VisualElement>("collapse-button");
            collapseButton.GetFirstAncestorOfType<VisualElement>().Remove(collapseButton);
        }

        private void RegisterDragAndDrop()
        {
            var d = new Dragger();
            d.target = this;
        }

        private void RegisterPositionCallback()
        {
            // NOTE This might not be the most performant variant to update every nodes position on mouse move
            RegisterCallback<MouseMoveEvent>(evt => node.position = GetPosition().position);
        }

        public void CreateFields(ComponentData analysisData)
        {
            // Properties
            analysisData.properties.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new MemberView(data, true, true, value));
            });

            // Slicer
            if (analysisData.methods.Count > 0)
            {
                var slicer = new VisualElement();
                slicer.AddToClassList("node-slicer");
                extensionContainer.Add(slicer);
            }

            // Methods
            analysisData.methods.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new MethodView(data, value));
            });

            // Slicer
            if (analysisData.triggers.Count > 0)
            {
                var slicer = new VisualElement();
                slicer.AddToClassList("node-slicer");
                extensionContainer.Add(slicer);
            }

            // Triggers
            analysisData.triggers.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new MemberView(data, false, true, value));
            });
        }
    }
}
