using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class InitialValueAdapter : AdapterBase
    {
        [SerializeField] private SerializableMemberInfo info;
        [SerializeField] private bool isProperty;
        [SerializeField] private SerializableObject initialValue;

        public InitialValueAdapter(string nodeId, SerializableMemberInfo info, object initialValue) : base(nodeId)
        {
            this.info = info;
            isProperty = info.memberInfo is PropertyInfo;
            this.initialValue = new SerializableObject(initialValue);
        }

        public void SetValue(object instance)
        {
            if (isProperty)
                ((PropertyInfo) info).SetValue(instance, initialValue.@object);
            else
                ((FieldInfo) info).SetValue(instance, initialValue.@object);
        }
    }
}
