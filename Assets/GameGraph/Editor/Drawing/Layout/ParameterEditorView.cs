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
        private BlackboardSection section;

        public void Initialize(EditorGameGraph graph)
        {
            this.graph = graph;

            title = this.GetWindow().titleContent.text;
            subTitle = EditorConstants.BlackboardSubHeadline;

            section = new BlackboardSection {headerVisible = false};
            RegisterAddElement();
            RegisterMoveElement();
            DrawParameters();
            Add(section);
        }

        private void RegisterAddElement()
        {
            addItemRequested += blackboard =>
            {
                var parameterView = new ParameterView();
                parameterView.graph = graph;
                section.Add(parameterView);
                parameterView.Initialize(typeof(object));
                parameterView.PersistState();
                // Show edit name field immediately
                parameterView.nameView.OpenTextEditor();
                graph.isDirty = true;
            };
        }

        private void RegisterMoveElement()
        {
            moveItemRequested += (blackboard, newIndex, element) =>
                element.GetFirstOfType<ParameterView>().MoveToIndex(newIndex);
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
                section.Add(parameterView);
                parameterView.Initialize(parameter);
            });
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ParameterEditorView, UxmlTraits>
        {
        }
    }
}
