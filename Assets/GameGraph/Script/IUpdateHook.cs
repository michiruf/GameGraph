namespace GameGraph
{
    public interface IUpdateHook
    {
        void Update();
        void LateUpdate();
        void FixedUpdate();
    }
}
