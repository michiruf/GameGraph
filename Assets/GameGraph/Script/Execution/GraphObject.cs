using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph
{
    public class GraphObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public Dictionary<string, Node> nodes;

        [SerializeField] private List<string> nodeInternalKeys;
        [SerializeField] private List<Node> nodeInternalValues;

        public void ConstructGraph()
        {
            // To speed this up, nodes.AsParallel().ForEach() would be pretty nice,
            // but since initial values like Time.deltaTime on fields require
            // Unity's main thread, this would be impossible to do
            foreach (var pair in nodes)
            {
                pair.Value.ConstructInstance();
            }
            foreach (var pair in nodes)
            {
                pair.Value.SetupExecutionAdapterLinks(nodes);
            }
        }

        public void Start()
        {
            foreach (var pair in nodes)
            {
                (pair.Value.instance as IStartHook)?.Start();
            }
        }

        public void Update()
        {
            foreach (var pair in nodes)
            {
                (pair.Value.instance as IUpdateHook)?.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var pair in nodes)
            {
                (pair.Value.instance as IUpdateHook)?.LateUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var pair in nodes)
            {
                (pair.Value.instance as IUpdateHook)?.FixedUpdate();
            }
        }

        public void OnBeforeSerialize()
        {
            nodeInternalKeys = new List<string>();
            nodeInternalValues = new List<Node>();
            foreach (var pair in nodes)
            {
                nodeInternalKeys.Add(pair.Key);
                nodeInternalValues.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            if (nodeInternalKeys.Count != nodeInternalValues.Count)
                throw new Exception("Entry List have different sizes!");

            nodes = new Dictionary<string, Node>();
            for (var i = 0; i < nodeInternalKeys.Count; i++)
                nodes.Add(nodeInternalKeys[i], nodeInternalValues[i]);
        }
    }
}
