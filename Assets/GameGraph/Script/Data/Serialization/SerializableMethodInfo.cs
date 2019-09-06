using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableMethodInfo
    {
        [SerializeField] private SerializableType serializableType;
        [SerializeField] private string methodName;

        public SerializableMethodInfo(MethodInfo info)
        {
            serializableType = info.DeclaringType.ToSerializable();
            methodName = info.Name;
        }

        public MethodInfo ToMethodInfo()
        {
            var type = serializableType.ToType();
            return type.GetMethod(methodName);
        }
    }

    public static class SerializableMethodInfoExtension
    {
        public static SerializableMethodInfo ToSerializable(this MethodInfo info)
        {
            return new SerializableMethodInfo(info);
        }
    }
}
