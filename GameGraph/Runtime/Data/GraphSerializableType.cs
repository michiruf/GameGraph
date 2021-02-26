using System;
using JetBrains.Annotations;

namespace GameGraph
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    // TODO Maybe change the naming of SerializableType inside Serialization folder to UnitySerializableType,
    //      since this class does the same, but not with the Unity specific hooks
    // This class should be used in blocks to serialize types. For an example see GetComponent block.
    public class GraphSerializableType
    {
        public Type type
        {
            get => Type.GetType(assemblyQualifiedName);
            set => assemblyQualifiedName = value.AssemblyQualifiedName;
        }
        public string assemblyQualifiedName;

        public GraphSerializableType()
        {
            // Used for serialization
        }

        public GraphSerializableType(Type type)
        {
            this.type = type;
        }
    }
}
