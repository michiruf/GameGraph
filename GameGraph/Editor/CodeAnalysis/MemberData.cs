using System.Reflection;

namespace GameGraph.Editor
{
    public struct MemberData<T> where T : MemberInfo
    {
        public readonly T info;
        public readonly bool isRequired;

        public MemberData(T info)
        {
            this.info = info;
            isRequired = info.GetCustomAttribute<RequiredAttribute>() != null;
        }
    }
}
