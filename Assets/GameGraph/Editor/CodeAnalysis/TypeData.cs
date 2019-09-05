using System;

namespace GameGraph.Editor
{
    public class TypeData
    {
        public readonly string name;
        public readonly string assemblyQualifiedName;
        public readonly Type type;

        public TypeData(Type type)
        {
            name = type.Name;
            assemblyQualifiedName = type.AssemblyQualifiedName;
            this.type = type;
        }
    }
}
