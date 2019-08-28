using TinyMessenger;

namespace GameGraph.Editor
{
    public class NodeAddEvent : ITinyMessage
    {
        public object Sender { get; }
        public readonly string name;

        public NodeAddEvent(string name)
        {
            this.name = name;
        }
    }
}
