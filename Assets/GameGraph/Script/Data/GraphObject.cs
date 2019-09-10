using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameGraph
{
    public class GraphObject : ScriptableObject
    {
        public Dictionary<string, Node> nodes
        {
            get => nodesInternal.dictionary;
            set => nodesInternal.dictionary = value;
        }
        [SerializeField] private StringNodeDictionary nodesInternal = new StringNodeDictionary();

        public Dictionary<string, Parameter> parameters
        {
            get => parametersInternal.dictionary;
            set => parametersInternal.dictionary = value;
        }
        [SerializeField] private StringParameterDictionary parametersInternal = new StringParameterDictionary();

        public void ConstructGraph(Dictionary<string, object> parameterInstances)
        {
            // To speed this up, nodes.AsParallel().ForEach() would be pretty nice,
            // but since initial values like Time.deltaTime on fields require
            // Unity's main thread, this would be impossible to do
            foreach (var pair in nodes)
            {
                pair.Value.ConstructOrReferenceInstance(parameterInstances);
            }
            foreach (var pair in nodes)
            {
                pair.Value.SetupInstanceAdapterLinks(nodes);
                pair.Value.SetupExecutionAdapterLinks(nodes);
            }
        }

        public void OrderNodesByExecutionOrder()
        {
            // TODO Order entries!
            var b = nodes
                .OrderBy(pair => (pair.Value.instance as IExecutionOrder)?.executionOrder ?? int.MaxValue);
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
    }
}
