using TinyMessenger;

namespace GameGraph.Editor
{
    public class NodeAddEvent : ITinyMessage
    {
        public object Sender { get; }
        public readonly TypeData typeData;

        public NodeAddEvent(TypeData typeData)
        {
            this.typeData = typeData;
        }
    }
}
