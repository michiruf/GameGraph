using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableMemberInfo : ISerializationCallbackReceiver
    {
        [SerializeField] private SerializableType type;
        [SerializeField] private string name;

        public MemberInfo memberInfo { get; private set; }
        public FieldInfo fieldInfo => memberInfo as FieldInfo;
        public PropertyInfo propertyInfo => memberInfo as PropertyInfo;
        public EventInfo eventInfo => memberInfo as EventInfo;

        public SerializableMemberInfo(MemberInfo info)
        {
            memberInfo = info;
        }

        public static implicit operator MemberInfo(SerializableMemberInfo i) => i.memberInfo;
        public static implicit operator SerializableMemberInfo(MemberInfo i) => new SerializableMemberInfo(i);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return memberInfo == ((SerializableMemberInfo) obj).memberInfo;
        }

        public override int GetHashCode()
        {
            return memberInfo.GetHashCode();
        }

        public void OnBeforeSerialize()
        {
            if (memberInfo == null)
                return;

            type = memberInfo.DeclaringType;
            name = memberInfo.Name;
        }

        public void OnAfterDeserialize()
        {
            if (type == null || type.type == null || name == null)
                return;

            memberInfo = type.type.GetMember(name, Constants.ReflectionFlags)[0];
        }
    }
}
