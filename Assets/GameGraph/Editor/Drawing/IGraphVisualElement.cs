namespace GameGraph.Editor
{
    public interface IGraphVisualElement
    {
        // TODO Instead of giving the window, provide a static method in the GameGraphWindow to find the corresponding window
        // TODO ... to one of its views (rootView should be fine)
        void Initialize(string graphName, EditorGameGraph graph, GameGraphWindow window);
    }
}
