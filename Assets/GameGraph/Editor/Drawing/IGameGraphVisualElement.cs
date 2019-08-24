namespace GameGraph.Editor
{
    public interface IGameGraphVisualElement
    {
        GameGraph graph { get; set; }

        void Initialize();
    }
}
