using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableMemberInfo
    {
        [SerializeField] private SerializableType serializableType;
        [SerializeField] private string memberName;

        public SerializableMemberInfo(MemberInfo info)
        {
            serializableType = info.DeclaringType.ToSerializable();
            memberName = info.Name;
        }

        public FieldInfo ToFieldInfo()
        {
            var type = serializableType.ToType();
            return type.GetField(memberName);
        }

        // TODO Use properties as well
        public PropertyInfo ToPropertyInfo()
        {
            var type = serializableType.ToType();
            return type.GetProperty(memberName);
        }

        public EventInfo ToEventInfo()
        {
            var type = serializableType.ToType();
            return type.GetEvent(memberName);
        }
    }

    public static class SerializableFieldInfoExtension
    {
        public static SerializableMemberInfo ToSerializable(this MemberInfo info)
        {
            return new SerializableMemberInfo(info);
        }
    }
}
