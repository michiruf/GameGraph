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
        // TODO Maybe decouple the representative data from the view
        // TODO ... to avoid persistance problems
        public GameGraph graph { get; set; }

        public void Initialize()
        {
            RegisterViewNavigation();
            DrawGraph();
            graph.graphChangedEvent += DrawGraph;

            // TODO Use this.graphViewChanged event
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
            // TODO Nothing is visible when activated
            // Clear already drawn graph
//            Clear();

            // Draw nodes
            graph.nodes.ForEach(node =>
            {
                var view = new NodeView();
                view.graph = graph;
                view.Initialize(node);
                AddElement(view);
            });
            
            // Bring edges to front
            // TODO Might not working
            edges.ForEach(edge => edge.BringToFront());
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
