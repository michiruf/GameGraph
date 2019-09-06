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
            return type.GetField(memberName, GameGraphConstants.ReflectionFlags);
        }

        // TODO Use properties as well
        public PropertyInfo ToPropertyInfo()
        {
            var type = serializableType.ToType();
            return type.GetProperty(memberName, GameGraphConstants.ReflectionFlags);
        }

        public EventInfo ToEventInfo()
        {
            var type = serializableType.ToType();
            return type.GetEvent(memberName, GameGraphConstants.ReflectionFlags);
        }

        public MemberInfo ToMemberInfo(int index = 0)
        {
            var type = serializableType.ToType();
            return type.GetMember(memberName, GameGraphConstants.ReflectionFlags)[index];
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
