using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class PropertyAdapter : AdapterBase
    {
        [SerializeField] private SerializableMemberInfo outputInfo;
        [SerializeField] private bool outputIsProperty;
        [SerializeField] private SerializableMemberInfo inputInfo;
        [SerializeField] private bool inputIsProperty;

        public PropertyAdapter(string outputNodeId, SerializableMemberInfo outputInfo, SerializableMemberInfo inputInfo)
            : base(outputNodeId)
        {
            this.outputInfo = outputInfo;
            this.inputInfo = inputInfo;
            outputIsProperty = outputInfo.memberInfo is PropertyInfo;
            inputIsProperty = inputInfo.memberInfo is PropertyInfo;
        }

        public void TransmitValue(object output, object input)
        {
            var value = outputIsProperty
                ? ((PropertyInfo) outputInfo).GetValue(output)
                : ((FieldInfo) outputInfo).GetValue(output);

            if (inputIsProperty)
                ((PropertyInfo) inputInfo).SetValue(input, value);
            else
                ((FieldInfo) inputInfo).SetValue(input, value);
        }
    }
}
