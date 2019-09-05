using System.Reflection;

namespace GameGraph.Editor
{
    public class MemberData<T> where T : MemberInfo
    {
        public readonly string name;
        public readonly T info;

        public MemberData(T info)
        {
            name = info.Name;
            this.info = info;
        }
    }
}
