using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace GameGraph.Editor
{
    public class EdgeView : Edge, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        private EditorEdge edge;

        public void Initialize(EditorEdge edge)
        {
            this.edge = edge;
            LinkPorts();
        }

        private void LinkPorts()
        {
            var container = GetFirstAncestorOfType<GraphEditorView>();
            var ports = container.ports.ToList();

            // NOTE This lookup may be inefficient, lookup node and then port
            input = ports.FirstOrDefault(port =>
                edge.inputNodeId.Equals(port.node.name) &&
                edge.inputPortName.Equals(port.name) &&
                port.direction == Direction.Input);
            input?.Connect(this);

            output = ports.FirstOrDefault(port =>
                edge.outputNodeId.Equals(port.node.name) &&
                edge.outputPortName.Equals(port.name) &&
                port.direction == Direction.Output);
            output?.Connect(this);

            // If a edge is still not connected to two ports, we have to remove it to
            // avoid data inconsistency
            if (input == null || output == null)
            {
                RemoveState();
                // NOTE Instead of removing this edge, maybe introduce an index for the port, so that
                // ... an invalid edge could be drawn?
                parent.Remove(this);
            }
        }

        public void PersistState()
        {
            if (edge == null)
                edge = new EditorEdge();

            edge.inputNodeId = input?.node?.name;
            edge.inputPortName = input?.name;
            edge.outputNodeId = output?.node?.name;
            edge.outputPortName = output?.name;

            if (!graph.edges.Contains(edge))
                graph.edges.Add(edge);
        }

        public void RemoveState()
        {
            if (edge != null)
                graph.edges.Remove(edge);
        }
    }
}
