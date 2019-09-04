using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameGraph
{
    [Serializable]
    public class Node : ISerializationCallbackReceiver
    {
        public Type classType;
        public List<PropertyAdapter> propertyAdapters;
        public List<ExecutionAdapter> executionAdapters;

        [SerializeField] private SerializableType serializableClassType;

        public object instance { get; private set; }

        public void ConstructInstance()
        {
            instance = Activator.CreateInstance(classType);
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
            serializableClassType = classType?.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            classType = serializableClassType?.ToType();
        }
    }
}
