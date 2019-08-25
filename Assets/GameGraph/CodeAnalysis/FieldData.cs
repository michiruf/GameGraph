using System;

namespace GameGraph.CodeAnalysis
{
    public struct FieldData
    {
        public string name;
        public Type type;

        public FieldData(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
