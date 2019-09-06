using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameGraph.Editor
{
    // TODO Rename this
    public struct ClassData
    {
        public readonly Type type;
        public readonly List<FieldInfo> fields;
        public readonly List<PropertyInfo> properties;
        public readonly List<EventInfo> events;
        public readonly List<MethodInfo> methods;

        public ClassData(
            Type type, 
            List<FieldInfo> fields, 
            List<PropertyInfo> properties, 
            List<EventInfo> events,
            List<MethodInfo> methods)
        {
            this.type = type;
            this.fields = fields;
            this.properties = properties;
            this.events = events;
            this.methods = methods;
        }
    }
}
