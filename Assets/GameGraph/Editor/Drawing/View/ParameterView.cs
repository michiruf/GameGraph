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

        public void Initialize(TypeData typeData)
        {
            Initialize(new EditorParameter(typeData));
        }

        private void Initialize()
        {
            nameView.text = parameter.name;
            typeButton.text = parameter.typeName;
            typeButton.clickable.clicked += () => CreateSearchWindow(typeData =>
            {
                typeButton.text = typeData.name;
                typeButton.userData = typeData.assemblyQualifiedName;
                PersistState();
            }, typeButton.clickable.lastMousePosition);
        }

        public void PersistState()
        {
            parameter.name = nameView.text;
            parameter.typeName = typeButton.text;
            parameter.typeAssemblyQualifiedName = typeButton.userData as string;

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

        private void CreateSearchWindow(Action<TypeData> callback, Vector2 position)
        {
            // NOTE The search window might be provided by the editor view to improve performance
            // NOTE ... Alternatively make static?
            var searchWindowProvider = ScriptableObject.CreateInstance<ReferenceSearchWindowProvider>();
            searchWindowProvider.Initialize(callback);
            SearchWindow.Open(new SearchWindowContext(position), searchWindowProvider);
        }
    }
}
