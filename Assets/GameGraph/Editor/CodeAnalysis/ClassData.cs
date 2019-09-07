using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct ClassData
    {
        // TODO Maybe use serializable data from here on, to have a unified and failsafe data handling?!
        public readonly Type type;
        public bool isGameGraphComponent; // TODO Put this here instead if the EditorParameter
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
