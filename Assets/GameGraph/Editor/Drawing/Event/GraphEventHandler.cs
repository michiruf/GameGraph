using System.Collections.Generic;
using TinyMessenger;

namespace GameGraph.Editor
{
    public static class GraphEventHandler
    {
        private static readonly Dictionary<EditorGameGraph, TinyMessengerHub> Handlers =
            new Dictionary<EditorGameGraph, TinyMessengerHub>();

        public static TinyMessengerHub Get(EditorGameGraph graph)
        {
            if (!Handlers.ContainsKey(graph))
                Handlers.Add(graph, new TinyMessengerHub());
            return Handlers[graph];
        }
    }
}
