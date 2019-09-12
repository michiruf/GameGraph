using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class Node
    {
        public List<InstanceAdapter> instanceAdapters;
        public List<ExecutionAdapter> executionAdapters;
        public List<PropertyAdapter> propertyAdapters;
        [SerializeField] private string parameterId;
        [SerializeField] private SerializableType type;

        public Node(SerializableType type, string parameterId)
        {
            this.type = type;
            this.parameterId = parameterId;
        }

        public object ConstructOrFindParameter(Dictionary<string, object> parameterInstances)
        {
            if (!string.IsNullOrEmpty(parameterId) && parameterInstances.ContainsKey(parameterId))
            {
                return parameterInstances[parameterId];
            }
            return Activator.CreateInstance(type.type);
        }

        public void SetupInstanceAdapterLinks(object instance, Dictionary<string, object> nodeInstances)
        {
            instanceAdapters.ForEach(adapter =>
            {
                adapter.TransmitValue(nodeInstances[adapter.outputNodeId], instance);
            });
        }

        public void SetupExecutionAdapterLinks(object instance, Dictionary<string, object> nodeInstances)
        {
            executionAdapters.ForEach(adapter =>
            {
                adapter.LinkAction(nodeInstances[adapter.outputNodeId], instance,
                    () => FetchProperties(instance, nodeInstances));
            });
        }

        private void FetchProperties(object instance, Dictionary<string, object> nodeInstances)
        {
            propertyAdapters.ForEach(adapter =>
            {
                adapter.TransmitValue(nodeInstances[adapter.outputNodeId], instance);
            });
        }
    }
}
