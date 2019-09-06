using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO Maybe change back to BlackboardRows and make the collapsible again (might be nicer)
    public class ParameterView : VisualElement, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        private EditorParameter parameter;

        private BlackboardNameField nameView => this.QCached<BlackboardNameField>("name");
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
            Initialize(new EditorParameter(type.Name, type));
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
            }, typeButton.clickable.lastMousePosition); // TODO Position is not correct at all
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
}
