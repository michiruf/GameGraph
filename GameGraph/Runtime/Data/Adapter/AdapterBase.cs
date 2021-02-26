namespace GameGraph
{
    public abstract class AdapterBase
    {
        // Since we only need this id in this context we abuse the adapter as a data holder for easiness
        public string outputNodeId;

        protected AdapterBase(string outputNodeId)
        {
            this.outputNodeId = outputNodeId;
        }
    }
}
