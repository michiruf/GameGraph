using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class Node
    {
        public List<InstanceAdapter> instanceAdapters;
        public List<InitialValueAdapter> initialValueAdapters;
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
            // TODO The second condition should not be present, because we want this to fail early
            //      But for any reason, if we skip this check and use a TryGetValue, it
            //      is not triggered properly, so we cannot log information for now
            //      To reproduce do not assign a parameter of a GameGraphBehaviour
            if (!string.IsNullOrEmpty(parameterId) && parameterInstances.ContainsKey(parameterId))
            {
                return parameterInstances[parameterId];
            }
            return Activator.CreateInstance(type.type);
        }

        public void SetInitialValues(object instance)
        {
            initialValueAdapters.ForEach(adapter => adapter.SetValue(instance));
        }

        public void SetupInstanceAdapterLinks(object instance, Dictionary<string, object> nodeInstances)
        {
            instanceAdapters.ForEach(adapter =>
                adapter.TransmitValue(nodeInstances[adapter.outputNodeId], instance));
        }

        public void SetupExecutionAdapterLinks(object instance, Dictionary<string, object> nodeInstances,
            Dictionary<string, Node> nodes)
        {
            executionAdapters.ForEach(adapter =>
                adapter.LinkAction(
                    nodeInstances[adapter.outputNodeId], instance,
                    () => FetchPropertiesRecursive(instance, nodeInstances, nodes)));
        }

        internal void FetchPropertiesRecursive(object instance, Dictionary<string, object> nodeInstances,
            Dictionary<string, Node> nodes)
        {
            // NOTE This might be a bit overkill. Would it be better to fetch only the one property needed?
            //      Would it be better to only activate this behaviour for specific edges via attribute in classes?
            propertyAdapters.ForEach(adapter =>
            {
                var recursiveFetchInputNode = nodes[adapter.outputNodeId];
                var recursiveFetchInputNodeInstance = nodeInstances[adapter.outputNodeId];
                recursiveFetchInputNode.FetchPropertiesRecursive(recursiveFetchInputNodeInstance, nodeInstances, nodes);
            });
            FetchProperties(instance, nodeInstances);
        }

        private void FetchProperties(object instance, Dictionary<string, object> nodeInstances)
        {
            propertyAdapters.ForEach(adapter =>
                adapter.TransmitValue(nodeInstances[adapter.outputNodeId], instance));
        }
    }
}
