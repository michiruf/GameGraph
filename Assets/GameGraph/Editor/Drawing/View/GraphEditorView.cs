using GameGraph.Editor.Util;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // ReSharper disable once UnusedMember.Global
    public class GraphEditorView : GraphView, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        public GraphEditorView()
        {
            InitializeUi();
        }
        
        private void InitializeUi()
        {
            const string stylePath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/GraphEditorView.uss";
            this.AddStylesheet(stylePath);
            const string layoutPath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/GraphEditorView.uxml";
            this.AddLayout(layoutPath);
        }

        // Used by Unity magically
        // ReSharper disable once UnusedMember.Global
        public new class UxmlFactory : UxmlFactory<GraphEditorView>
        {
        }
    }
}
