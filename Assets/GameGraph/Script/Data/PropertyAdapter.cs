using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class PropertyAdapter
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;
        [SerializeField] private bool outputIsProperty;
        [SerializeField] private bool inputIsProperty;
        [SerializeField] private SerializableMemberInfo outputInfo;
        [SerializeField] private SerializableMemberInfo inputInfo;

        public PropertyAdapter(string outputNodeId, SerializableMemberInfo outputInfo, SerializableMemberInfo inputInfo)
        {
            this.outputNodeId = outputNodeId;
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
