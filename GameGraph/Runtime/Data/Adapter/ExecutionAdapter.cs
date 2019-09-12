using System;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class ExecutionAdapter : AdapterBase
    {
        [SerializeField] private SerializableMemberInfo outputEventInfo;
        [SerializeField] private SerializableMethodInfo inputMethodInfo;

        public ExecutionAdapter(string outputNodeId, SerializableMemberInfo outputEventInfo,
            SerializableMethodInfo inputMethodInfo) : base(outputNodeId)
        {
            this.outputEventInfo = outputEventInfo;
            this.inputMethodInfo = inputMethodInfo;
        }

        public void LinkAction(object output, object input, Action beforeInvokeLink)
        {
            void InternalAction()
            {
                beforeInvokeLink?.Invoke();
                inputMethodInfo.methodInfo.Invoke(input, null);
            }

            outputEventInfo.eventInfo.AddMethod.Invoke(output, new object[] {(Action) InternalAction});
        }
    }
}
