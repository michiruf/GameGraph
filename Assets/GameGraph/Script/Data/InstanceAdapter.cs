using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class InstanceAdapter
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;
        [SerializeField] private bool inputIsProperty;
        [SerializeField] private SerializableMemberInfo inputInfo;

        public InstanceAdapter(string outputNodeId, SerializableMemberInfo inputInfo)
        {
            this.outputNodeId = outputNodeId;
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
