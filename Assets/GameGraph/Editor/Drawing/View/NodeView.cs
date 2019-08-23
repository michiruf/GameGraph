namespace GameGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        public NodeView()
        {
            InitializeUi();
        }
        
        private void InitializeUi()
        {
            const string stylePath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/NodeView.uss";
            this.AddStylesheet(stylePath);
            const string layoutPath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/NodeView.uxml";
            this.AddLayout(layoutPath);
        }
    }
}
