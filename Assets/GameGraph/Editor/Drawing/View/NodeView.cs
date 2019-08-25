using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public GameGraph graph { get; set; }

        private bool dragActive;

        public void Initialize(Node node)
        {
            title = node.name;
            expanded = true;
            // TODO Remove dev position setting
            if (node.position == default)
                node.position = Random.insideUnitCircle * 300f + Vector2.one * 300f;
            SetPosition(new Rect(node.position, Vector2.one * 400));
            RegisterDragAndDrop();

            var analysisData = CodeAnalysis.GetComponentData(node.name);
            CreateFields(node, analysisData);
            // NOTE May fix this
            // For any reason if there is just one element, the layout is misbehaving
            extensionContainer.Add(new VisualElement());
            RefreshExpandedState();
        }

        public void CreateFields(Node node, ComponentData analysisData)
        {
            // Properties
            analysisData.properties.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new FieldView(data, true, true, value));
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
                extensionContainer.Add(new FieldView(data, false, true, value));
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
        }

        private void RegisterDragAndDrop()
        {
            var d = new Dragger();
            d.target = this;
        }
    }
}
