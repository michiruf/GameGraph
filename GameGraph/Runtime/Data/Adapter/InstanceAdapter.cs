using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class InstanceAdapter : AdapterBase
    {
        [SerializeField] private SerializableMemberInfo inputInfo;
        [SerializeField] private bool inputIsProperty;

        public InstanceAdapter(string outputNodeId, SerializableMemberInfo inputInfo) : base(outputNodeId)
        {
            this.inputInfo = inputInfo;
            inputIsProperty = inputInfo.memberInfo is PropertyInfo;
        }

        public void TransmitValue(object output, object input)
        {
            if (inputIsProperty)
                ((PropertyInfo) inputInfo).SetValue(input, output);
            else
                ((FieldInfo) inputInfo).SetValue(input, output);
        }
    }
}
