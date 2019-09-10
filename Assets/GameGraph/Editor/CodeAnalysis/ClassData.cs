using System.Collections.Generic;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct ClassData
    {
        public readonly TypeData type;
        public readonly List<MemberData<FieldInfo>> fields;
        public readonly List<MemberData<PropertyInfo>> properties;
        public readonly List<MemberData<EventInfo>> events;
        public readonly List<MethodData> methods;

        public ClassData(
            TypeData type,
            List<MemberData<FieldInfo>> fields,
            List<MemberData<PropertyInfo>> properties,
            List<MemberData<EventInfo>> events,
            List<MethodData> methods)
        {
            this.type = type;
            this.fields = fields;
            this.properties = properties;
            this.events = events;
            this.methods = methods;
        }
    }
}
