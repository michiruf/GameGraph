using UnityEngine;

namespace GameGraph.Editor
{
    public static class GraphTransformExtension
    {
        public static GraphObject ToExecutableGraph(this RawGameGraph graph)
        {
            var graphObject = ScriptableObject.CreateInstance<GraphObject>();
            return graphObject;
        }
    }
}
