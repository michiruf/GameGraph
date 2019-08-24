using System.Collections.Generic;

namespace GameGraph
{
    public struct ComponentData
    {
        public readonly List<FieldData> properties;
        public readonly List<FieldData> triggers;
        public readonly List<MethodData> methods;

        public ComponentData(
            List<FieldData> properties,
            List<FieldData> triggers,
            List<MethodData> methods)
        {
            this.properties = properties;
            this.triggers = triggers;
            this.methods = methods;
        }
    }
}
