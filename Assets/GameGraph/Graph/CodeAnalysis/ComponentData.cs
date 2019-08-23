using System;
using System.Collections.Generic;

namespace GameGraph
{
    public class ComponentData
    {
        private readonly List<Tuple<string, FieldType>> properties;
        private readonly List<Tuple<string, FieldType>> triggers;
        private readonly List<Tuple<string, FieldType>> methods;

        public ComponentData(
            List<Tuple<string, FieldType>> properties,
            List<Tuple<string, FieldType>> triggers,
            List<Tuple<string, FieldType>> methods)
        {
            this.properties = properties;
            this.triggers = triggers;
            this.methods = methods;
        }
    }
}
