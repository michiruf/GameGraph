using System;
using UnityEditor;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawEdge
    {
        public string id;
        private string ingoingLinkNodeId;
        private string outgoingLinkNodeId;

        public RawEdge()
        {
            id = GUID.Generate().ToString();
        }

        public RawEdge(string id, string ingoingLinkNodeId, string outgoingLinkNodeId) : this()
        {
            this.id = id;
            this.ingoingLinkNodeId = ingoingLinkNodeId;
            this.outgoingLinkNodeId = outgoingLinkNodeId;
        }
    }
}
