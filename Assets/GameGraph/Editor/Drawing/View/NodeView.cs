using System;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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
            SetPosition(new Rect(node.position, Vector2.one * 200));

            CreateFields(node);
            RefreshExpandedState();

            RegisterDragAndDrop();

            return;
            var port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Vector3));
        }

        public void CreateFields(Node node)
        {
            var analysisData = CodeAnalysis.GetComponentData(node.name);

            analysisData.properties.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new FieldField(data, value));
            });

            var slicer1 = new VisualElement();
            slicer1.AddToClassList("slicer");
            extensionContainer.Add(slicer1);

            analysisData.triggers.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new FieldField(data, value));
            });

            var slicer2 = new VisualElement();
            slicer2.AddToClassList("slicer");
            extensionContainer.Add(slicer2);

            analysisData.properties.ForEach(data =>
            {
                node.data.TryGetValue(data.name, out var value);
                extensionContainer.Add(new MethodField(data, value));
            });
        }

        private void RegisterDragAndDrop()
        {
            var d = new Dragger();
            d.target = this;
        }
    }
}
