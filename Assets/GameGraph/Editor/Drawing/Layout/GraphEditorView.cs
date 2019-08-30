using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class GraphEditorView : GraphView, IGraphVisualElement
    {
        private RawGameGraph graph;

        public void Initialize(RawGameGraph graph)
        {
            this.graph = graph;

            // Register change events
            graphViewChanged += change =>
            {
                change.movedElements?.ForEach(element =>
                {
                    if (!(element is IGraphElement graphElement))
                        return;
                    graphElement.graph = graph;
                    graphElement.PersistState();
                });
                change.edgesToCreate?.ForEach(element =>
                {
                    if (!(element is IGraphElement graphElement))
                        return;
                    graphElement.graph = graph;
                    graphElement.PersistState();
                });
                change.elementsToRemove?.ForEach(element =>
                {
                    if (!(element is IGraphElement graphElement))
                        return;
                    graphElement.graph = graph;
                    graphElement.RemoveState();
                });
                return change;
            };

            // Register add event
            var graphEventHandler = GraphEventHandler.Get(graph);
            graphEventHandler.Subscribe<NodeAddEvent>(e =>
            {
                var nodeView = new NodeView();
                nodeView.Initialize(e.name);
                AddElement(nodeView);
            });
            
            RegisterViewNavigation();
            DrawGraph();
        }

        private void RegisterViewNavigation()
        {
            var d = new ContentDragger();
            d.target = this;
            var z = new ContentZoomer();
            z.target = this;
        }

        private void DrawGraph()
        {
            // Clear previous drawn stuff first
            nodes.ForEach(RemoveElement);
            edges.ForEach(RemoveElement);

            // Draw nodes
            graph.nodes.ForEach(node =>
            {
                var nodeView = new NodeView();
                AddElement(nodeView);
                nodeView.Initialize(node);
            });

            // Draw edges
            graph.edges.ForEach(edge =>
            {
                var edgeView = new EdgeView();
                AddElement(edgeView);
                edgeView.Initialize(edge);
            });
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(port =>
                        port.direction != startPort.direction
                        && port.node != startPort.node
                        && port.portType == startPort.portType
                    // TODO Why was this in parent method?
                    //&& nodeAdapter.GetAdapter(port.source, startPort.source) != null
                )
                .ToList();
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<GraphEditorView>
        {
        }
    }
}
