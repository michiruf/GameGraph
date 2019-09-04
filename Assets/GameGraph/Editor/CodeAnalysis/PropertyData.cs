using System;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct PropertyData
    {
        public readonly string name;
        public readonly Type type;
        public readonly FieldInfo info;

        public PropertyData(FieldInfo info)
        {
            name = info.Name;
            type = info.FieldType;
            this.info = info;
        }
    }
}
