using System.Collections.Generic;

namespace GameGraph.CodeAnalysis
{
    public struct ComponentData
    {
        public readonly List<MemberData> properties;
        public readonly List<MemberData> triggers;
        public readonly List<MethodData> methods;

        public ComponentData(
            List<MemberData> properties,
            List<MemberData> triggers,
            List<MethodData> methods)
        {
            this.properties = properties;
            this.triggers = triggers;
            this.methods = methods;
        }
    }
}
