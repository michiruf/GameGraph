using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class ParameterEditorView : Blackboard, IGraphVisualElement
    {
        private EditorGameGraph graph;

        public void Initialize(string graphName, EditorGameGraph graph, GameGraphWindow window)
        {
            this.graph = graph;

            title = graphName;
            subTitle = EditorConstants.BlackboardSubHeadline;

            RegisterAddElement();
            RegisterMoveElement();
            RegisterEditText();
            DrawParameters();
        }

        private void RegisterAddElement()
        {
            addItemRequested += blackboard =>
            {
                var parameterView = new ParameterView();
                parameterView.graph = graph;
                Add(parameterView);
                parameterView.Initialize(typeof(object));
                parameterView.PersistState();
            };
        }

        private void RegisterMoveElement()
        {
            moveItemRequested += (blackboard, newIndex, element) => { Debug.LogError("moveItemRequested"); };
        }

        private void RegisterEditText()
        {
            editTextRequested += (blackboard, element, newText) =>
                element.GetFirstOfType<ParameterView>().OnRenameParameter(newText);
        }

        private void DrawParameters()
        {
            // Clear previous drawn stuff first
            contentContainer.Clear();

            // Draw parameters (by a clone of the list to be able to modify)
            graph.parameters.ToList().ForEach(parameter =>
            {
                var parameterView = new ParameterView();
                parameterView.graph = graph;
                Add(parameterView);
                parameterView.Initialize(parameter);
            });
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ParameterEditorView, UxmlTraits>
        {
        }
    }
}
