using System.Collections.Generic;
using TinyMessenger;

namespace GameGraph.Editor
{
    public class GraphEventHandler
    {
        private static readonly Dictionary<string, TinyMessengerHub> Handlers =
            new Dictionary<string, TinyMessengerHub>();

        public static TinyMessengerHub Get(string id)
        {
            if (!Handlers.ContainsKey(id))
                Handlers.Add(id, new TinyMessengerHub());
            return Handlers[id];
        }
    }
}
