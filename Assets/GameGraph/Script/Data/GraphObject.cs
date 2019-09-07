using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameGraph
{
    public class GraphObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized] public Dictionary<string, Node> nodes;
        [NonSerialized] public Dictionary<string, Parameter> parameters;

        [SerializeField] private List<string> nodeInternalKeys;
        [SerializeField] private List<Node> nodeInternalValues;
        [SerializeField] private List<string> parametersInternalKeys;
        [SerializeField] private List<Parameter> parametersInternalValues;

        public T GetInstance<T>() where T : class
        {
            return nodes.FirstOrDefault(pair => pair.Value.instance is T) as T;
        }

        public List<T> GetInstances<T>() where T : class
        {
            return nodes
                .Select(pair => pair.Value.instance as T)
                .Where(arg => arg != null)
                .ToList();
        }

        public void OnBeforeSerialize()
        {
            Assert.IsNotNull(nodes, "Nodes should never be null!");
            nodeInternalKeys = new List<string>();
            nodeInternalValues = new List<Node>();
            foreach (var pair in nodes)
            {
                nodeInternalKeys.Add(pair.Key);
                nodeInternalValues.Add(pair.Value);
            }

            Assert.IsNotNull(parameters, "Parameters should never be null!");
            parametersInternalKeys = new List<string>();
            parametersInternalValues = new List<Parameter>();
            foreach (var pair in parameters)
            {
                parametersInternalKeys.Add(pair.Key);
                parametersInternalValues.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            if (nodeInternalKeys.Count != nodeInternalValues.Count)
                throw new Exception("Entry List have different sizes!");
            nodes = new Dictionary<string, Node>();
            for (var i = 0; i < nodeInternalKeys.Count; i++)
                nodes.Add(nodeInternalKeys[i], nodeInternalValues[i]);

            if (parametersInternalKeys.Count != parametersInternalValues.Count)
                throw new Exception("Entry List have different sizes!");
            parameters = new Dictionary<string, Parameter>();
            for (var i = 0; i < parametersInternalKeys.Count; i++)
                parameters.Add(parametersInternalKeys[i], parametersInternalValues[i]);
        }
    }
}
