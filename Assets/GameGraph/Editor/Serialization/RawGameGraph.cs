using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawGameGraph
    {
        private string idInternal;
        private bool isDirtyInternal;
        public List<RawNode> nodes = new List<RawNode>();
        public List<RawEdge> edges = new List<RawEdge>();

        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(idInternal))
                    idInternal = GUID.Generate().ToString();
                return idInternal;
            }
        }

        public bool isDirty => isDirty ||
                               nodes.Aggregate(false, (b, node) => b || node.isDirty) ||
                               edges.Aggregate(false, (b, edge) => b || edge.isDirty);
    }
}
