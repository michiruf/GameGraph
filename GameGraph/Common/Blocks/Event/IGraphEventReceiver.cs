namespace GameGraph.Common.Blocks
{
    public interface IGraphEventReceiver
    {
        // Annotated just in case this analysis will be enabled in the future (looking upwards)
        [ExcludeFromGraph]
        void OnEventInvoked();
    }
}
