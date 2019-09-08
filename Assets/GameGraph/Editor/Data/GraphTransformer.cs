using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameGraph.Editor
{
    public class GraphTransformer
    {
        private readonly EditorGameGraph graph;
        [Obsolete] private Dictionary<string, Tuple<ClassData, Node, List<EditorEdge>>> nodeData;

        // Node data
        private readonly Dictionary<string, ClassData> nodeClassData = new Dictionary<string, ClassData>();
        private readonly Dictionary<string, List<EditorEdge>> nodeEdgesToNode =
            new Dictionary<string, List<EditorEdge>>();
        private readonly Dictionary<string, Node> nodeResult = new Dictionary<string, Node>();

        // Parameter data
        private readonly Dictionary<string, Parameter> parameterResult = new Dictionary<string, Parameter>();

        public GraphTransformer(EditorGameGraph graph)
        {
            this.graph = graph;
        }

        private void CollectNodeData()
        {
            graph.nodes
                .ForEach(editorNode =>
                {
                    nodeClassData.Add(editorNode.id, CodeAnalyzer.GetNodeData(editorNode.type));
                    nodeEdgesToNode.Add(editorNode.id, graph.edges
                        .Where(editorEdge => editorEdge.inputNodeId.Equals(editorNode.id))
                        .ToList());
                    nodeResult.Add(editorNode.id, new Node(editorNode.type, editorNode.parameterId));
                });
        }

        private void BuildNodes()
        {
            nodeResult.ToList()
                .ForEach(pair =>
                {
                    var id = pair.Key;
                    var node = pair.Value;
                    var inputClass = nodeClassData[id];
                    var edgesToNode = nodeEdgesToNode[id];

                    node.instanceAdapters = edgesToNode
                        .Where(editorEdge => editorEdge.outputPortName.Equals(EditorConstants.ParameterPortId))
                        .Select(editorEdge =>
                        {
                            var inputProperty = inputClass.fields
                                .Select(info => (MemberInfo) info)
                                .Concat(inputClass.properties)
                                .FirstOrDefault(data => data.Name.Equals(editorEdge.inputPortName));
                            return Tuple.Create(
                                editorEdge.outputNodeId,
                                inputProperty
                            );
                        })
                        .Where(tuple => tuple.Item2 != null)
                        .Select(tuple => new InstanceAdapter(tuple.Item1, tuple.Item2))
                        .ToList();

                    node.propertyAdapters = edgesToNode
                        .Select(editorEdge =>
                        {
                            var outputClass = nodeClassData[editorEdge.outputNodeId];
                            var outputProperty = outputClass.fields
                                .Select(info => (MemberInfo) info)
                                .Concat(outputClass.properties)
                                .FirstOrDefault(data => data.Name.Equals(editorEdge.outputPortName));
                            var inputProperty = inputClass.fields
                                .Select(info => (MemberInfo) info)
                                .Concat(inputClass.properties)
                                .FirstOrDefault(data => data.Name.Equals(editorEdge.inputPortName));
                            return Tuple.Create(
                                editorEdge.outputNodeId,
                                outputProperty,
                                inputProperty
                            );
                        })
                        .Where(tuple => tuple.Item2 != null && tuple.Item3 != null)
                        .Select(tuple => new PropertyAdapter(tuple.Item1, tuple.Item2, tuple.Item3))
                        .ToList();

                    node.executionAdapters = edgesToNode
                        .Select(editorEdge =>
                        {
                            var outputClass = nodeClassData[editorEdge.outputNodeId];
                            return Tuple.Create(
                                editorEdge.outputNodeId,
                                outputClass.events.FirstOrDefault(data => data.Name.Equals(editorEdge.outputPortName)),
                                inputClass.methods?.FirstOrDefault(data => data.Name.Equals(editorEdge.inputPortName))
                            );
                        })
                        .Where(tuple => tuple.Item2 != null && tuple.Item3 != null)
                        .Select(tuple => new ExecutionAdapter(tuple.Item1, tuple.Item2, tuple.Item3))
                        .ToList();
                });
        }

        private void BuildParameterData()
        {
            graph.parameters
                .ForEach(editorParameter =>
                    parameterResult.Add(editorParameter.id, new Parameter(editorParameter.name, editorParameter.type)));
        }

        public GraphObject GetGraphObject(GraphObject previous)
        {
            CollectNodeData();
            BuildNodes();
            BuildParameterData();

            var graphObject = previous != null ? previous : ScriptableObject.CreateInstance<GraphObject>();
            graphObject.nodes = nodeResult;
            graphObject.parameters = parameterResult;
            return graphObject;
        }
    }

    public static class GraphTransformExtension
    {
        public static GraphObject ToExecutableGraph(this EditorGameGraph graph, GraphObject previous)
        {
            return new GraphTransformer(graph).GetGraphObject(previous);
        }
    }
}
