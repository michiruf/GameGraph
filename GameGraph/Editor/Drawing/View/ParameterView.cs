using System;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class ParameterView : VisualElement, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        public EditorParameter parameter { get; private set; }
        private bool planedDetachFromPanel;

        internal BlackboardField nameView => this.QCached<BlackboardField>("name");
        private TextField nameEditView => nameView.QCached<TextField>();
        private Button typeButton => this.QCached<Button>("type");

        private TypeSearchWindowProvider parameterTypeSearchWindowProvider => this.GetUserDataOrCreate(() =>
        {
            // Bottleneck is not reflection, but creation of the search window,
            // so no need to cache this for all instances
            var data = CodeAnalyzer.GetNodeTypes().Concat(CodeAnalyzer.GetNonNodeTypes());
            var provider = ScriptableObject.CreateInstance<TypeSearchWindowProvider>();
            provider.Initialize(EditorConstants.ParameterSearchWindowHeadline, data);
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

            // Handle rename event
            nameEditView.RegisterValueChangedCallback(evt =>
            {
                nameView.text = evt.newValue;
                PersistState();
                this.GetEventBus().Dispatch(new ParameterChangedEvent(parameter));
            });

            // Register delete callback (why is this so hard?!)
            // This also removes the state when the window is unloaded, but then no persistence should occur...
            nameView.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                if (planedDetachFromPanel) return;
                RemoveState();
                RemoveFromHierarchy();
            });
        }

        public void MoveToIndex(int index)
        {
            // Move the view
            planedDetachFromPanel = true;
            var parent = this.parent;
            var oldIndex = parent.IndexOf(this);
            var realIndex = oldIndex > index ? index : index - 1;
            RemoveFromHierarchy();
            parent.Insert(realIndex, this);
            planedDetachFromPanel = false;

            // Handle the state
            graph.parameters.Remove(parameter);
            graph.parameters.Insert(realIndex, parameter);
            graph.isDirty = true;
            PersistState();
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

        private void CreateSearchWindow(Action<Type> callback, Vector2 position)
        {
            parameterTypeSearchWindowProvider.callback = (type, vector2) => callback.Invoke(type);
            SearchWindow.Open(new SearchWindowContext(position), parameterTypeSearchWindowProvider);
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
