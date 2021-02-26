namespace GameGraph.Editor
{
    public class ParameterChangedEvent
    {
        public readonly EditorParameter parameter;

        public ParameterChangedEvent(EditorParameter parameter)
        {
            this.parameter = parameter;
        }
    }
}
