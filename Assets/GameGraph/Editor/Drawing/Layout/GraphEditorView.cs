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
        private EditorGameGraph graph;

        public void Initialize(EditorGameGraph graph)
        {
            this.graph = graph;
            
            RegisterViewNavigation();
            DrawGraph();

            // Register change events (after draw!)
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
                AddGraphElement(nodeView);
                nodeView.Initialize(e.typeData);
            });
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

            // Draw nodes (by a clone of the list to be able to modify)
            graph.nodes.ToList().ForEach(node =>
            {
                var nodeView = new NodeView();
                AddGraphElement(nodeView);
                nodeView.Initialize(node);
            });

            // Draw edges (by a clone of the list to be able to modify)
            graph.edges.ToList().ForEach(edge =>
            {
                var edgeView = new EdgeView();
                AddGraphElement(edgeView);
                edgeView.Initialize(edge);
            });
        }

        public void AddGraphElement(GraphElement element)
        {
            if (element is IGraphElement graphElement)
                graphElement.graph = graph;
            AddElement(element);
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
