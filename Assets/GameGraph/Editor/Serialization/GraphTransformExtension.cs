using System;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    public static class GraphTransformExtension
    {
        public static GraphObject ToExecutableGraph(this RawGameGraph rawGraph)
        {
            var graphObject = ScriptableObject.CreateInstance<GraphObject>();

            var preprocess = rawGraph.nodes
                .ToDictionary(rawNode => rawNode.id, rawNode =>
                {
                    var analysisType = CodeAnalyzer.GetTypeFromAllAssemblies(rawNode.name);
                    var analysisData = CodeAnalyzer.GetComponentData(analysisType);
                    var node = new Node {classType = analysisType};
                    return Tuple.Create(analysisData, node);
                });

            graphObject.nodes = preprocess.ToDictionary(pair => pair.Key, pair =>
            {
                var (inputAnalysisData, node) = pair.Value;

                var edgesToNode = rawGraph.edges
                    .Where(rawEdge => rawEdge.inputNodeId.Equals(pair.Key))
                    .ToList();

                node.propertyAdapters = edgesToNode
                    .Select(rawEdge => Tuple.Create(
                        rawEdge.outputNodeId,
                        preprocess[rawEdge.outputNodeId]?.Item1.properties?
                            .FirstOrDefault(data => data.name.Equals(rawEdge.outputPortId)),
                        inputAnalysisData.properties?.FirstOrDefault(data => data.name.Equals(rawEdge.inputPortId))
                    ))
                    .Where(tuple => tuple.Item2?.info != null && tuple.Item3?.info != null)
                    .Select(tuple => new PropertyAdapter(tuple.Item1, tuple.Item2?.info, tuple.Item3?.info))
                    .ToList();

                node.executionAdapters = edgesToNode
                    .Select(rawEdge => Tuple.Create(
                        rawEdge.outputNodeId,
                        preprocess[rawEdge.outputNodeId]?.Item1.events?
                            .FirstOrDefault(data => data.name.Equals(rawEdge.outputPortId)),
                        inputAnalysisData.methods?.FirstOrDefault(data => data.name.Equals(rawEdge.inputPortId))
                    ))
                    .Where(tuple => tuple.Item2?.info != null && tuple.Item3?.info != null)
                    .Select(tuple => new ExecutionAdapter(tuple.Item1, tuple.Item2?.info, tuple.Item3?.info))
                    .ToList();

                return node;
            });

            return graphObject;
        }
    }
}
