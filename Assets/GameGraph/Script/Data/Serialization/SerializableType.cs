using System;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableType
    {
        [SerializeField] private string name;
        [SerializeField] private string assemblyQualifiedName;

        public SerializableType(Type type)
        {
            name = type.Name;
            assemblyQualifiedName = type.AssemblyQualifiedName;
        }

        public Type ToType()
        {
            return string.IsNullOrEmpty(assemblyQualifiedName)
                ? Type.GetType(assemblyQualifiedName)
                : Type.GetType(name);
        }
    }

    public static class SerializableTypeExtension
    {
        public static SerializableType ToSerializable(this Type type)
        {
            return type != null ? new SerializableType(type) : null;
        }
    }
}
