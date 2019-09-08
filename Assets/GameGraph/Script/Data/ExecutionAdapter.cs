using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class ExecutionAdapter
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;
        [SerializeField] private SerializableMemberInfo outputEventInfo;
        [SerializeField] private SerializableMethodInfo inputMethodInfo;

        public ExecutionAdapter(
            string outputNodeId, SerializableMemberInfo outputEventInfo, SerializableMethodInfo inputMethodInfo)
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
                inputMethodInfo.methodInfo.Invoke(input, null);
            }

            outputEventInfo.eventInfo.AddMethod.Invoke(output, new object[] {(Action) InternalAction});
        }
    }
}
