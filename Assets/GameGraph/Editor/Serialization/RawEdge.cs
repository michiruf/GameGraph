using System;
using UnityEditor;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawEdge
    {
        private string idInternal;
        public string ingoingLinkNodeId;
        public string outgoingLinkNodeId;
        public bool isDirty;

        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(idInternal))
                    idInternal = GUID.Generate().ToString();
                return idInternal;
            }
        }

        public RawEdge()
        {
        }

        public RawEdge(string ingoingLinkNodeId, string outgoingLinkNodeId) : this()
        {
            this.ingoingLinkNodeId = ingoingLinkNodeId;
            this.outgoingLinkNodeId = outgoingLinkNodeId;
        }
    }
}
