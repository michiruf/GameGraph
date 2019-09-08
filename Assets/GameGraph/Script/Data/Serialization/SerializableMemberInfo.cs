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

        // NOTE Are the MemberInfo operators not enough?
        public static implicit operator MemberInfo(SerializableMemberInfo s) => s.memberInfo;
        //public static implicit operator FieldInfo(SerializableMemberInfo s) => s.fieldInfo;
        //public static implicit operator PropertyInfo(SerializableMemberInfo s) => s.propertyInfo;
        //public static implicit operator EventInfo(SerializableMemberInfo s) => s.eventInfo;
        public static implicit operator SerializableMemberInfo(MemberInfo i) => new SerializableMemberInfo(i);
        //public static implicit operator SerializableMemberInfo(FieldInfo i) => new SerializableMemberInfo(i);
        //public static implicit operator SerializableMemberInfo(PropertyInfo i) => new SerializableMemberInfo(i);
        //public static implicit operator SerializableMemberInfo(EventInfo i) => new SerializableMemberInfo(i);

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
            type = memberInfo.DeclaringType;
            name = memberInfo.Name;
        }

        public void OnAfterDeserialize()
        {
            memberInfo = type.type.GetMember(name, GameGraphConstants.ReflectionFlags)[0];
        }
    }
}
