using System;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class ParameterView : VisualElement, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        private EditorParameter parameter;

        private BlackboardField nameView => this.QCached<BlackboardField>("name");
        private Button typeButton => this.QCached<Button>("type");

        public ParameterView()
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/ParameterView.uxml");
        }

        public void Initialize(EditorParameter parameter)
        {
            this.parameter = parameter;
            Initialize();
        }

        public void Initialize(Type type)
        {
            Initialize(new EditorParameter(type.Name + "Reference", type));
        }

        private void Initialize()
        {
            nameView.text = parameter.name;
            typeButton.text = parameter.type.Name;
            typeButton.clickable.clicked += () => CreateSearchWindow(type =>
            {
                typeButton.text = type.Name;
                typeButton.userData = type;
                PersistState();
            }, typeButton.worldBound.position); // TODO Position is not correct at all

            // Register delete callback (why is this so hard?!)
            // This also removes the state when the window is unloaded, but then no persistence should occur...
            nameView.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                RemoveState();
                RemoveFromHierarchy();
            });

            // TODO Register dragging to graph view
            // @see UnityEditor.ShaderGraph.Drawing.BlackboardProvider#OnDragUpdatedEvent
            // For now, just add it anywhere
            var clickable = new Clickable(() =>
            {
                var graphContainer = GetFirstOfType<GraphEditorView>();

                var nodeView = new NodeView();
                nodeView.graph = graph;
                graphContainer.AddElement(nodeView);
                nodeView.Initialize(parameter.type, Vector2.zero, parameter.id); // TODO Set position correctly
                nodeView.PersistState();
            });
            nameView.AddManipulator(clickable);
        }

        public void PersistState()
        {
            parameter.name = nameView.text;
            // Only update the data when the button was clicked once
            parameter.type = typeButton.userData as Type ?? parameter.type;

            if (!graph.parameters.Contains(parameter))
                graph.parameters.Add(parameter);
        }

        public void RemoveState()
        {
            graph.parameters.Remove(parameter);
        }

        public void OnRenameParameter(string newName)
        {
            nameView.text = newName;
            PersistState();
        }

        private void CreateSearchWindow(Action<Type> callback, Vector2 position)
        {
            // NOTE The search window might be provided by the editor view to improve performance
            // NOTE ... Alternatively make static?
            var searchWindowProvider = ScriptableObject.CreateInstance<ParameterSearchWindowProvider>();
            searchWindowProvider.Initialize(callback);
            SearchWindow.Open(new SearchWindowContext(position), searchWindowProvider);
        }
    }

    [UsedImplicitly]
    internal class BlackboardField : UnityEditor.Experimental.GraphView.BlackboardField
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<BlackboardField>
        {
        }
    }
}
