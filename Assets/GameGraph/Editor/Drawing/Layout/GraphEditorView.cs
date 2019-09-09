using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class GraphEditorView : GraphView, IGraphVisualElement
    {
        private EditorGameGraph graph;
        private EventBus eventBus;

        private TypeSearchWindowProvider nodeTypeSearchWindowProvider => this.GetUserDataOrCreate(() =>
        {
            var data = CodeAnalyzer.GetNodeTypes();
            var provider = ScriptableObject.CreateInstance<TypeSearchWindowProvider>();
            provider.Initialize(EditorConstants.NodeSearchWindowHeadline, data);
            return provider;
        });

        public void Initialize(EditorGameGraph graph)
        {
            this.graph = graph;

            RegisterViewNavigation();
            RegisterAddElement();
            DrawGraph();
            RegisterChangeEvent(); // After draw!
        }

        private void RegisterViewNavigation()
        {
            var d = new ContentDragger();
            d.target = this;
            var z = new ContentZoomer();
            z.target = this;
        }

        private void RegisterAddElement()
        {
            nodeTypeSearchWindowProvider.callback = (type, position) =>
            {
                var realPosition = contentViewContainer.GetContainerRelativePosition(position);

                var nodeView = new NodeView();
                nodeView.graph = graph;
                AddElement(nodeView);
                nodeView.Initialize(type, realPosition, null);
                nodeView.PersistState();
            };
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition),
                    nodeTypeSearchWindowProvider);
            };
        }

        private void RegisterChangeEvent()
        {
            graphViewChanged += change =>
            {
                Enumerable.Empty<GraphElement>()
                    .Concat(change.movedElements ?? Enumerable.Empty<GraphElement>())
                    .Concat(change.edgesToCreate ?? Enumerable.Empty<GraphElement>())
                    .ToList()
                    .ForEach(element =>
                    {
                        if (!(element is IGraphElement graphElement))
                            return;
                        graphElement.graph = graph;
                        graphElement.PersistState();
                    });
                change.elementsToRemove?
                    .ForEach(element =>
                    {
                        if (!(element is IGraphElement graphElement))
                            return;
                        graphElement.graph = graph;
                        graphElement.RemoveState();
                    });
                return change;
            };
        }

        private void DrawGraph()
        {
            // Clear previous drawn stuff first
            nodes.ForEach(RemoveElement);
            edges.ForEach(RemoveElement);

            // Draw nodes (by a clone of the list to be able to modify)
            graph.nodes.ToList().ForEach(node =>
            {
                var nodeView = new NodeView();
                nodeView.graph = graph;
                AddElement(nodeView);
                nodeView.Initialize(node);
            });

            // Draw edges (by a clone of the list to be able to modify)
            graph.edges.ToList().ForEach(edge =>
            {
                var edgeView = new EdgeView();
                edgeView.graph = graph;
                AddElement(edgeView);
                edgeView.Initialize(edge);
            });
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(port =>
                    port.direction != startPort.direction && port.node != startPort.node &&
                    port.portType == startPort.portType)
                .ToList();
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<GraphEditorView>
        {
        }
    }
}
