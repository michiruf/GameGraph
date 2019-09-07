namespace GameGraph.Editor
{
    public interface IAttached
    {
        // TODO Eliminate those initialize methods by handling in the window the attached event and call this method
        //      And give a proper name

        void OnAfterAddToHierarchy();
    }
}
