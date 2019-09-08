namespace GameGraph
{
    public class GraphExecutor
    {
        private readonly GraphObject graph;

        public GraphExecutor(GraphObject graph)
        {
            this.graph = graph;
        }

        public void ConstructGraph()
        {
            // To speed this up, nodes.AsParallel().ForEach() would be pretty nice,
            // but since initial values like Time.deltaTime on fields require
            // Unity's main thread, this would be impossible to do
            foreach (var pair in graph.nodes)
            {
                pair.Value.ConstructOrReferenceInstance(graph.parameters);
            }
            foreach (var pair in graph.nodes)
            {
                pair.Value.SetupInstanceAdapterLinks(graph.nodes);
                pair.Value.SetupExecutionAdapterLinks(graph.nodes);
            }
        }

        public void Start()
        {
            foreach (var pair in graph.nodes)
            {
                (pair.Value.instance as IStartHook)?.Start();
            }
        }

        public void Update()
        {
            foreach (var pair in graph.nodes)
            {
                (pair.Value.instance as IUpdateHook)?.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var pair in graph.nodes)
            {
                (pair.Value.instance as IUpdateHook)?.LateUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var pair in graph.nodes)
            {
                (pair.Value.instance as IUpdateHook)?.FixedUpdate();
            }
        }
    }
}
