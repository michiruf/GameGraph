using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class PropertyAdapter : ISerializationCallbackReceiver
    {
        public string outputNodeId;
        public FieldInfo outputFieldInfo;
        public FieldInfo inputFieldInfo;

        [SerializeField] private SerializableMemberInfo serializableOutputInfo;
        [SerializeField] private SerializableMemberInfo serializableInputInfo;

        public PropertyAdapter(string outputNodeId, FieldInfo outputFieldInfo, FieldInfo inputFieldInfo)
        {
            this.outputNodeId = outputNodeId;
            this.outputFieldInfo = outputFieldInfo;
            this.inputFieldInfo = inputFieldInfo;
        }

        public void TransmitValue(object output, object input)
        {
            var value = outputFieldInfo.GetValue(output);
            inputFieldInfo.SetValue(input, value);
        }

        public void OnBeforeSerialize()
        {
            if (outputFieldInfo != null)
                serializableOutputInfo = outputFieldInfo.ToSerializable();
            if (inputFieldInfo != null)
                serializableInputInfo = inputFieldInfo.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            if (serializableOutputInfo != null)
                outputFieldInfo = serializableOutputInfo.ToFieldInfo();
            if (serializableInputInfo != null)
                inputFieldInfo = serializableInputInfo.ToFieldInfo();
        }
    }
}
