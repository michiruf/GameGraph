using System;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    public static class GraphTransformExtension
    {
        public static GraphObject ToExecutableGraph(this EditorGameGraph editorGraph)
        {
            var graphObject = ScriptableObject.CreateInstance<GraphObject>();

            var preprocess = editorGraph.nodes
                .ToDictionary(editorNode => editorNode.id, editorNode =>
                {
                    var analysisData = CodeAnalyzer.GetNodeData(editorNode.typeAssemblyQualifiedName);
                    var node = new Node {classType = analysisData.typeData.type};
                    return Tuple.Create(analysisData, node);
                });

            graphObject.nodes = preprocess.ToDictionary(pair => pair.Key, pair =>
            {
                var (inputAnalysisData, node) = pair.Value;

                var edgesToNode = editorGraph.edges
                    .Where(editorEdge => editorEdge.inputNodeId.Equals(pair.Key))
                    .ToList();

                node.propertyAdapters = edgesToNode
                    .Select(editorEdge => Tuple.Create(
                        editorEdge.outputNodeId,
                        preprocess[editorEdge.outputNodeId]?.Item1.fields?
                            .FirstOrDefault(data => data.name.Equals(editorEdge.outputPortId)),
                        inputAnalysisData.fields?.FirstOrDefault(data => data.name.Equals(editorEdge.inputPortId))
                    ))
                    .Where(tuple => tuple.Item2?.info != null && tuple.Item3?.info != null)
                    .Select(tuple => new PropertyAdapter(tuple.Item1, tuple.Item2?.info, tuple.Item3?.info))
                    .ToList();

                node.executionAdapters = edgesToNode
                    .Select(editorEdge => Tuple.Create(
                        editorEdge.outputNodeId,
                        preprocess[editorEdge.outputNodeId]?.Item1.events?
                            .FirstOrDefault(data => data.name.Equals(editorEdge.outputPortId)),
                        inputAnalysisData.methods?.FirstOrDefault(data => data.name.Equals(editorEdge.inputPortId))
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
