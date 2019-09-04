using System.Collections.Generic;

namespace GameGraph.Editor
{
    public struct ComponentData
    {
        public readonly List<PropertyData> properties;
        public readonly List<EventData> events;
        public readonly List<MethodData> methods;

        public ComponentData(
            List<PropertyData> properties,
            List<EventData> events,
            List<MethodData> methods)
        {
            this.properties = properties;
            this.events = events;
            this.methods = methods;
        }
    }
}
