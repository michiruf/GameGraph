namespace GameGraph.Editor
{
    public interface IGraphElement
    {
        RawGameGraph graph { set; }
        void PersistState();
        void RemoveState();
    }
}
