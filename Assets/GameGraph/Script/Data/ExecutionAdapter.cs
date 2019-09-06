using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class ExecutionAdapter : ISerializationCallbackReceiver
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;
        [SerializeField] private SerializableMemberInfo serializableOutputInfo;
        [SerializeField] private SerializableMethodInfo serializableInputInfo;

        private EventInfo outputEventInfo;
        private MethodInfo inputMethodInfo;

        public ExecutionAdapter(string outputNodeId, EventInfo outputEventInfo, MethodInfo inputMethodInfo)
        {
            this.outputNodeId = outputNodeId;
            this.outputEventInfo = outputEventInfo;
            this.inputMethodInfo = inputMethodInfo;
        }

        public void LinkAction(object output, object input, Action beforeInvokeLink)
        {
            void InternalAction()
            {
                beforeInvokeLink?.Invoke();
                inputMethodInfo.Invoke(input, null);
            }

            outputEventInfo.AddMethod.Invoke(output, new object[] {(Action) InternalAction});
        }

        public void OnBeforeSerialize()
        {
            if (outputEventInfo != null)
                serializableOutputInfo = outputEventInfo.ToSerializable();
            if (inputMethodInfo != null)
                serializableInputInfo = inputMethodInfo.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            if (serializableOutputInfo != null)
                outputEventInfo = serializableOutputInfo.ToEventInfo();
            if (serializableInputInfo != null)
                inputMethodInfo = serializableInputInfo.ToMethodInfo();
        }
    }
}
