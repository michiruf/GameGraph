using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

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

        public object instance { get; private set; }

        public Node(SerializableType type, string parameterId)
        {
            this.type = type;
            this.parameterId = parameterId;
        }

        public void ConstructOrReferenceInstance(Dictionary<string, Parameter> parameters)
        {
            if (!string.IsNullOrEmpty(parameterId) && parameters.ContainsKey(parameterId))
            {
                instance = parameters[parameterId].instance;
                return;
            }

            instance = Activator.CreateInstance(type.type);
        }

        public void SetupInstanceAdapterLinks(Dictionary<string, Node> nodes)
        {
            instanceAdapters.ForEach(adapter =>
            {
                nodes.TryGetValue(adapter.outputNodeId, out var outputNode);
                Assert.IsNotNull(outputNode);
                adapter.TransmitValue(outputNode.instance, instance);
            });
        }

        public void SetupExecutionAdapterLinks(Dictionary<string, Node> nodes)
        {
            executionAdapters.ForEach(adapter =>
            {
                nodes.TryGetValue(adapter.outputNodeId, out var outputNode);
                Assert.IsNotNull(outputNode);
                adapter.LinkAction(outputNode.instance, instance, () => FetchProperties(nodes));
            });
        }

        private void FetchProperties(Dictionary<string, Node> nodes)
        {
            propertyAdapters.ForEach(adapter =>
            {
                nodes.TryGetValue(adapter.outputNodeId, out var outputNode);
                Assert.IsNotNull(outputNode);
                adapter.TransmitValue(outputNode.instance, instance);
            });
        }
    }
}
