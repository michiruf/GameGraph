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
        [SerializeField] private object initialValue;

        public InitialValueAdapter(string nodeId, SerializableMemberInfo info, object initialValue) : base(nodeId)
        {
            this.info = info;
            isProperty = info.memberInfo is PropertyInfo;
            this.initialValue = initialValue;
        }

        public void SetValue(object instance)
        {
            if (isProperty)
                ((PropertyInfo) info).SetValue(instance, initialValue);
            else
                ((FieldInfo) info).SetValue(instance, initialValue);
        }
    }
}
