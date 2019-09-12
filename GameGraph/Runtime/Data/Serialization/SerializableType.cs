using System;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] private string assemblyQualifiedName;

        public Type type { get; private set; }

        public SerializableType(Type type)
        {
            this.type = type;
        }

        public static implicit operator Type(SerializableType i) => i.type;
        public static implicit operator SerializableType(Type i) => new SerializableType(i);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return type == ((SerializableType) obj).type;
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }

        public void OnBeforeSerialize()
        {
            assemblyQualifiedName = type.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            type = Type.GetType(assemblyQualifiedName);
        }
    }
}
