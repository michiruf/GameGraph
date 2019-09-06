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
            subTitle = GameGraphEditorConstants.BlackboardSubHeadline;


            RegisterAddElement();
            RegisterMoveElement();
            RegisterEditText();
            DrawParameters();

            //AddBlockAddButtons();
        }

        private void RegisterAddElement()
        {
            addItemRequested += blackboard =>
            {
                var parameterView = new ParameterView();
                parameterView.graph = graph;
                parameterView.Initialize(new TypeData(typeof(string)));
                parameterView.PersistState();

                var row = new BlackboardRow(parameterView, new Label("Test"));
                Add(row);
            };
        }

        private void RegisterMoveElement()
        {
            moveItemRequested += (blackboard, newIndex, element) => { };
        }

        private void RegisterEditText()
        {
            editTextRequested += (blackboard, element, newText) =>
            { };
        }
        
        

        private void SEARCH_WINDOW_TODO(GameGraphWindow window)
        {
            // TODO HERE I AM
            
            var searchWindowProvider = ScriptableObject.CreateInstance<ReferenceSearchWindowProvider>();
            searchWindowProvider.Initialize(typeData =>
            {
                // TODO Rename currently selected element
            });
//            nodeCreationRequest += context =>
//            {
//                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
//            };
        }

        private void DrawParameters()
        {
            // TODO
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ParameterEditorView, UxmlTraits>
        {
        }
    }
}
