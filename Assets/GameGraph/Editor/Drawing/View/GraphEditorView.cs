using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class GraphEditorView : GraphView, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        public void Initialize()
        {
            RegisterViewNavigation();
            DrawGraph();
            graph.graphChangedEvent += DrawGraph;
        }

        private void RegisterViewNavigation()
        {
            var d = new ContentDragger();
            d.target = this;
            var z = new ContentZoomer();
            z.target = this;
        }

        private void DrawGraph()
        {
            // TODO Nothing is visible when activated
            //Debug.Log("Clearing drawn graph");
            //Clear();
            //Debug.Log("Clearing drawn graph done");
            
            Debug.Log("Drawing graph");
            graph.nodes.ForEach(node =>
            {
                var view = new NodeView();
                view.graph = graph;
                view.Initialize(node);
                AddElement(view);
            });
            Debug.Log("Drawing graph done");
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<GraphEditorView>
        {
        }
    }
}
