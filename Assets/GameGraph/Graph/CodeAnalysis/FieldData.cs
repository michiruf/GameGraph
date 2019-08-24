using System;

namespace GameGraph
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