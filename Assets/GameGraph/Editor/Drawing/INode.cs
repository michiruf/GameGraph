namespace GameGraph.Editor
{
    public interface INode
    {
        RawGameGraph graph { get; }
        RawNode node { get; }

        void Initialize();

        void Save();
    }
}
