using System;
using System.Collections.Generic;

namespace GameGraph
{
    [Serializable]
    public class GameGraph : UnityEngine.Object
    {
        public List<Node> nodes;
        public List<Link> links;
    }
}
