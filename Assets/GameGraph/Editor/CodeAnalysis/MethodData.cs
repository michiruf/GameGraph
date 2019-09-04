using System;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct MethodData
    {
        public readonly string name;
        public Type returnType; // TODO Use this anyhow?
        public ParameterInfo[] parameters; // TODO Use this anyhow?
        public readonly MethodInfo info;

        public MethodData(MethodInfo info)
        {
            name = info.Name;
            returnType = info.ReturnType;
            parameters = info.GetParameters();
            this.info = info;
        }
    }
}
