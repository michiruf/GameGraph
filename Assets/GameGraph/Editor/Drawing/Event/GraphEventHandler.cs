using System.Collections.Generic;
using TinyMessenger;

namespace GameGraph.Editor
{
    public static class GraphEventHandler
    {
        private static readonly Dictionary<RawGameGraph, TinyMessengerHub> Handlers =
            new Dictionary<RawGameGraph, TinyMessengerHub>();

        public static TinyMessengerHub Get(RawGameGraph graph)
        {
            if (!Handlers.ContainsKey(graph))
                Handlers.Add(graph, new TinyMessengerHub());
            return Handlers[graph];
        }
    }
}
