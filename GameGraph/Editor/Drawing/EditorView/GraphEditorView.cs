using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class GraphEditorView : GraphView, IGraphVisualElement
    {
        private EditorGameGraph graph;

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
            RegisterAddNode();
            RegisterAddParameter();
            DrawGraph();
            RegisterChangeEvent(); // After draw!

            this.GetEventBus().Register(this);
        }

        private void RegisterViewNavigation()
        {
            // The order of this manipulators is important
            // The code is copied from the game graph and enhanced by the content zoomer in the first line
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ClickSelector());
        }

        private void RegisterAddNode()
        {
            nodeTypeSearchWindowProvider.callback = (type, position) =>
            {
                var realPosition = contentViewContainer.GetContainerRelativePosition(position);

                var nodeView = new NodeView();
                nodeView.graph = graph;
                AddElement(nodeView);
                nodeView.Initialize(type, realPosition, null);
                nodeView.PersistState();
                this.GetEventBus().Dispatch(new GraphChangedEvent());
            };
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition),
                    nodeTypeSearchWindowProvider);
            };
        }

        private void RegisterAddParameter()
        {
            RegisterCallback<DragUpdatedEvent>(evt =>
            {
                if (!(DragAndDrop.GetGenericData("DragSelection") is List<ISelectable> selection))
                    return;
                if (selection.OfType<BlackboardField>().Any())
                    // Visual mode is necessary for the perform event to work!
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            });

            RegisterCallback<DragPerformEvent>(evt =>
            {
                if (!(DragAndDrop.GetGenericData("DragSelection") is List<ISelectable> selection))
                    return;

                foreach (var field in selection.OfType<BlackboardField>())
                {
                    var localPos = (evt.currentTarget as VisualElement).ChangeCoordinatesTo(
                        contentViewContainer, evt.localMousePosition);

                    var parameter = field.GetFirstOfType<ParameterView>().parameter;
                    var nodeView = new NodeView();
                    nodeView.graph = graph;
                    AddElement(nodeView);
                    nodeView.Initialize(parameter.type, localPos, parameter.id);
                    nodeView.PersistState();
                    this.GetEventBus().Dispatch(new GraphChangedEvent());
                }
            });
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
                    .ForEach(e =>
                    {
                        // Get the first IGraphElement above
                        VisualElement element = e;
                        while (!(element is IGraphElement))
                            element = element.parent;

                        var graphElement = (IGraphElement) element;
                        graphElement.graph = graph;
                        graphElement.RemoveState();

                        // Also remove the view from hierarchy (used for parameters)
                        element.RemoveFromHierarchy();
                    });

                // Flag the graph dirty if an element got removed, because this is not tracked by parents
                if (change.elementsToRemove?.Count > 0)
                    graph.isDirty = true;

                // Raise an event to check for all changed events
                this.GetEventBus().Dispatch(new GraphChangedEvent());

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
                    // TODO Maybe disallow assignment of Action (execution lines) with objects?
                    (port.portType.IsAssignableFrom(startPort.portType) || startPort.portType.IsAssignableFrom(port.portType))
                )
                .ToList();
        }

        [UsedImplicitly]
        public void OnEvent(ParameterChangedEvent e)
        {
            // Chain any event into a graph changed event
            this.GetEventBus().Dispatch(new GraphChangedEvent());
        }

        [UsedImplicitly]
        public void OnEvent(ControlValueChangedEvent e)
        {
            // Chain any event into a graph changed event
            this.GetEventBus().Dispatch(new GraphChangedEvent());
        }

        [UsedImplicitly]
        public void OnEvent(GraphChangedEvent e)
        {
            this.GetWindow<GameGraphWindow>().MayAutoSaveGraph();
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<GraphEditorView>
        {
        }
    }
}
