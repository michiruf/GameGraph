using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace GameGraph
{
    [Serializable]
    public class Node : ISerializationCallbackReceiver
    {
        public List<PropertyAdapter> propertyAdapters;
        public List<ExecutionAdapter> executionAdapters;
        public string parameterId;
        [SerializeField] private SerializableType serializableType;

        private Type type;
        public object instance { get; private set; }

        public Node(Type type, string parameterId)
        {
            this.type = type;
            this.parameterId = parameterId;
        }

        public void ConstructOrReceiveInstance(Dictionary<string, Parameter> parameters)
        {
            if (!string.IsNullOrEmpty(parameterId) && parameters.ContainsKey(parameterId))
            {
                instance = parameters[parameterId].instance;
                return;
            }

            instance = Activator.CreateInstance(type);
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

        public void OnBeforeSerialize()
        {
            serializableType = type.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            type = serializableType.ToType();
        }
    }
}
