using System.Collections.Generic;
using System.Linq;

namespace GameGraph
{
    public class GraphExecutor
    {
        private readonly GraphObject graph;
        private readonly Dictionary<string, object> parameterInstances;
        private readonly Dictionary<string, object> nodeInstances = new Dictionary<string, object>();

        private IOrderedEnumerable<KeyValuePair<string, object>> orderedNodeInstances;

        public GraphExecutor(GraphObject graph, Dictionary<string, object> parameterInstances)
        {
            this.graph = graph;
            this.parameterInstances = parameterInstances;
        }

        public void ConstructGraph()
        {
            // To speed this up, nodes.AsParallel().ForEach() would be pretty nice,
            // but since initial values like Time.deltaTime on fields require
            // Unity's main thread, this would be impossible to do
            foreach (var pair in graph.nodes)
                nodeInstances.Add(pair.Key, pair.Value.ConstructOrFindParameter(parameterInstances));
            foreach (var pair in graph.nodes)
            {
                var instance = nodeInstances[pair.Key];
                pair.Value.SetupInstanceAdapterLinks(instance, nodeInstances);
                pair.Value.SetupExecutionAdapterLinks(instance, nodeInstances, graph.nodes);
            }
        }

        public void OrderNodesByExecutionOrder()
        {
            orderedNodeInstances = nodeInstances
                .OrderBy(pair => (pair.Value as IExecutionOrder)?.executionOrder ?? int.MaxValue);
        }

        public void Start()
        {
            foreach (var pair in orderedNodeInstances)
            {
                (pair.Value as IStartHook)?.Start();
            }
        }

        public void Update()
        {
            foreach (var pair in orderedNodeInstances)
            {
                (pair.Value as IUpdateHook)?.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var pair in orderedNodeInstances)
            {
                (pair.Value as IUpdateHook)?.LateUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var pair in orderedNodeInstances)
            {
                (pair.Value as IUpdateHook)?.FixedUpdate();
            }
        }

        public T GetInstance<T>() where T : class
        {
            return nodeInstances.FirstOrDefault(pair => pair.Value is T) as T;
        }

        public List<T> GetInstances<T>() where T : class
        {
            return nodeInstances
                .Select(pair => pair.Value as T)
                .Where(arg => arg != null)
                .ToList();
        }
    }
}
