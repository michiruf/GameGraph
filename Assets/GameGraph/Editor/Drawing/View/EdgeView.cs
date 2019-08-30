using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class EdgeView : Edge, IGraphElement
    {
        public RawGameGraph graph { private get; set; }
        private RawEdge edge;

        private string ingoingLinkId => (input?.node as NodeView)?.node.id;
        private string outgoingLinkId => (output?.node as NodeView)?.node.id;

        public void Initialize(RawEdge edge)
        {
            this.edge = edge;

            var container = GetFirstAncestorOfType<GraphEditorView>();
            var nodes = container.nodes.ToList();
            var ingoingNode = nodes
                .FirstOrDefault(node => (node as NodeView)?.node.id.Equals(edge.ingoingLinkNodeId) ?? false);
            var outgoingNode = nodes
                .FirstOrDefault(node => (node as NodeView)?.node.id.Equals(edge.outgoingLinkNodeId) ?? false);

            // TODO First is completely wrong
            if (ingoingNode != default)
                input = ingoingNode.Query<Port>().First();
            if (outgoingNode != default)
                output = ingoingNode.Query<Port>().First();
            
            // TODO The edge does not get drawn, why?!
            MarkDirtyRepaint();
        }

        public void PersistState()
        {
            if (edge == null)
                edge = new RawEdge();

            edge.ingoingLinkNodeId = ingoingLinkId;
            edge.outgoingLinkNodeId = outgoingLinkId;

            if (!graph.edges.Contains(edge))
                graph.edges.Add(edge);
        }

        public void RemoveState()
        {
            graph.edges.Remove(edge);
        }
    }
}
