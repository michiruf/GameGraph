using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawGameGraph
    {
        public List<RawNode> nodes = new List<RawNode>();
        public List<RawEdge> edges = new List<RawEdge>();
    }
}
