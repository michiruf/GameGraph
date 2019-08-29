using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class GraphEditorView : GraphView, IGameGraphVisualElement
    {
        private RawGameGraph graph;

        public void Initialize(RawGameGraph graph)
        {
            this.graph = graph;

            RegisterViewNavigation();

            // Detects:
            // * Elements about to be removed
            // * Edges about to be created
            // * Elements already moved
            // * The delta of the last move
            // Does not detect that elements are added!
            graphViewChanged += change =>
            {
                SaveGraphState(change);
                DrawGraph();
                return change;
            };

            var graphEventHandler = GraphEventHandler.Get(graph.id);
            graphEventHandler.Subscribe<NodeAddEvent>(e =>
            {
                graph.nodes.Add(new RawNode(e.name));
                DrawGraph();
            });

            // Draw the graph initially
            DrawGraph();
        }

        private void RegisterViewNavigation()
        {
            var d = new ContentDragger();
            d.target = this;
            var z = new ContentZoomer();
            z.target = this;
        }

        private void SaveGraphState(GraphViewChange change)
        {
            if (change.movedElements?.Count > 0)
                nodes.ForEach(node => (node as INode)?.Save());
            if (change.edgesToCreate?.Count > 0)
                edges.ForEach(edge => (edge as IEdge)?.Save());
        }

        private void DrawGraph()
        {
            // Clear previous stuff first
            nodes.ForEach(RemoveElement);
            edges.ForEach(RemoveElement);

            // Draw nodes
            graph.nodes.ForEach(node =>
            {
                var nodeView = new NodeView();
                nodeView.node = node;
                nodeView.Initialize();
                AddElement(nodeView);
            });

            // Draw edges
            graph.edges.ForEach(edge =>
            {
                var edgeView = new EdgeView();
                edgeView.edge = edge;
                // TODO Initialize port connections
                AddElement(edgeView);
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
