namespace GameGraph.Common.Blocks
{
    public interface IGraphEventReceiver
    {
        // Annotated just in case this analysis will be enabled in the future (to look upwards in GameGraph objects)
        [ExcludeFromGraph]
        void OnEventInvoked();
    }
}
