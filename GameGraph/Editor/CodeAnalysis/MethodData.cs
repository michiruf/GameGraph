using System.Reflection;

namespace GameGraph.Editor
{
    public struct MethodData
    {
        public readonly MethodInfo info;

        public MethodData(MethodInfo info)
        {
            this.info = info;
        }
    }
}
