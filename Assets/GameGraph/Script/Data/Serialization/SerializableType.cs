using System;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableType
    {
        [SerializeField] private string assemblyQualifiedName;

        public SerializableType(Type type)
        {
            assemblyQualifiedName = type.AssemblyQualifiedName;
        }

        public Type ToType()
        {
            return Type.GetType(assemblyQualifiedName);
        }
    }

    public static class SerializableTypeExtension
    {
        public static SerializableType ToSerializable(this Type type)
        {
            return new SerializableType(type);
        }
    }
}
