using System.Collections.Generic;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct BlockData
    {
        public readonly TypeData typeData;
        public readonly List<MemberData<FieldInfo>> fields;
        public readonly List<MemberData<PropertyInfo>> properties;
        public readonly List<MemberData<EventInfo>> events;
        public readonly List<MemberData<MethodInfo>> methods;

        public BlockData(
            TypeData typeData,
            List<MemberData<FieldInfo>> fields,
            List<MemberData<PropertyInfo>> properties,
            List<MemberData<EventInfo>> events,
            List<MemberData<MethodInfo>> methods)
        {
            this.typeData = typeData;
            this.fields = fields;
            this.properties = properties;
            this.events = events;
            this.methods = methods;
        }
    }
}
