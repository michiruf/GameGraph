using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class PropertyAdapter : ISerializationCallbackReceiver
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;
        [SerializeField] private bool outputIsProperty;
        [SerializeField] private bool inputIsProperty;
        [SerializeField] private SerializableMemberInfo serializableOutputInfo;
        [SerializeField] private SerializableMemberInfo serializableInputInfo;

        private MemberInfo outputInfo;
        private MemberInfo inputInfo;

        public PropertyAdapter(string outputNodeId, MemberInfo outputInfo, MemberInfo inputInfo)
        {
            this.outputNodeId = outputNodeId;
            this.outputInfo = outputInfo;
            this.inputInfo = inputInfo;
            outputIsProperty = outputInfo is PropertyInfo;
            inputIsProperty = inputInfo is PropertyInfo;
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

        public void OnBeforeSerialize()
        {
            if (outputInfo != null)
                serializableOutputInfo = outputInfo.ToSerializable();
            if (inputInfo != null)
                serializableInputInfo = inputInfo.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            if (serializableOutputInfo != null)
                outputInfo = serializableOutputInfo.ToMemberInfo();
            if (serializableInputInfo != null)
                inputInfo = serializableInputInfo.ToMemberInfo();
            return;
            
            // TODO Remove more explicit way
            //if (serializableOutputInfo != null)
            //    outputInfo = outputIsProperty
            //        ? (MemberInfo) serializableOutputInfo.ToPropertyInfo()
            //        : serializableOutputInfo.ToFieldInfo();
            //if (serializableInputInfo != null)
            //    inputInfo = inputIsProperty
            //        ? (MemberInfo) serializableInputInfo.ToPropertyInfo()
            //        : serializableInputInfo.ToFieldInfo();
        }
    }
}
