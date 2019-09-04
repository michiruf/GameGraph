using System;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct EventData
    {
        public readonly string name;
        [Obsolete] public Type type;
        public readonly EventInfo info;

        public EventData(EventInfo info)
        {
            name = info.Name;
            type = info.EventHandlerType;
            this.info = info;
        }
    }
}
