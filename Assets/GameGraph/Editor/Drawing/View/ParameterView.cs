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

        private ParameterSearchWindowProvider parameterSearchWindowProvider => this.GetUserDataOrCreate(() =>
        {
            // Bottleneck is not reflection, but creation of the search window,
            // so no need to cache this for all instances
            var provider = ScriptableObject.CreateInstance<ParameterSearchWindowProvider>();
            provider.Initialize();
            return provider;
        });

        public ParameterView()
        {
            this.AddLayout(EditorConstants.ResourcesUxmlViewPath + "/ParameterView.uxml");
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
                }, typeButton.GetScreenPosition() + typeButton.clickable.lastMousePosition);

            // Register delete callback (why is this so hard?!)
            // This also removes the state when the window is unloaded, but then no persistence should occur...
            nameView.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                RemoveState();
                RemoveFromHierarchy();
            });

            // TODO Register dragging to graph view
            //      @see UnityEditor.ShaderGraph.Drawing.BlackboardProvider#OnDragUpdatedEvent
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
            this.GetEventBus().Dispatch(new ParameterChangedEvent(parameter));
        }

        private void CreateSearchWindow(Action<Type> callback, Vector2 position)
        {
            parameterSearchWindowProvider.callback = (type, vector2) => callback.Invoke(type);
            SearchWindow.Open(new SearchWindowContext(position), parameterSearchWindowProvider);
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
