namespace GameGraph.Editor
{
    public interface IGraphElement
    {
        EditorGameGraph graph { set; }

        void PersistState();

        void RemoveState();
    }
}
