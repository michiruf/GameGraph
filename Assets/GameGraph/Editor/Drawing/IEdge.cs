namespace GameGraph.Editor
{
    public interface IEdge
    {
        RawGameGraph graph { get; }
        RawEdge edge { get; }

        void Initialize();

        void Save();
    }
}
