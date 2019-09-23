using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableMethodInfo : ISerializationCallbackReceiver
    {
        [SerializeField] private SerializableType type;
        [SerializeField] private string name;

        public MethodInfo methodInfo { get; private set; }

        public SerializableMethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        public static implicit operator MethodInfo(SerializableMethodInfo i) => i.methodInfo;
        public static implicit operator SerializableMethodInfo(MethodInfo i) => new SerializableMethodInfo(i);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return methodInfo == ((SerializableMethodInfo) obj).methodInfo;
        }

        public override int GetHashCode()
        {
            if (methodInfo == null)
                return -1;

            return methodInfo.GetHashCode();
        }

        public void OnBeforeSerialize()
        {
            if (methodInfo == null)
                return;

            type = methodInfo.DeclaringType;
            name = methodInfo.Name;
        }

        public void OnAfterDeserialize()
        {
            if (type == null || type.type == null || name == null)
                return;

            methodInfo = type.type.GetMethod(name, Constants.ReflectionFlags);
        }
    }
}
