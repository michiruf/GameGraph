using UnityEditor.Experimental.GraphView;

namespace GameGraph.Editor
{
    public class EdgeView : Edge, IEdge
    {
        public RawGameGraph graph { get; set; }
        public RawEdge edge { get; set; }

        public void Initialize()
        {
            if (edge == null)
                return;

            // TODO Connect to the nodes
        }

        public void Save()
        {
            if (edge == null)
                edge = new RawEdge();

            if (input.node is INode inputPort)
                edge.ingoingLinkNodeId = inputPort.node.id;
            if (output.node is INode outputPort)
                edge.ingoingLinkNodeId = outputPort.node.id;

            if (!graph.edges.Contains(edge))
                graph.edges.Add(edge);
        }
    }
}
