using System;

namespace GameGraph.CodeAnalysis
{
    public struct MemberData
    {
        public string name;
        public Type type;

        public MemberData(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
