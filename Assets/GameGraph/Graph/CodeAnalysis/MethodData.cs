using System;
using System.Reflection;

namespace GameGraph
{
    public struct MethodData
    {
        public string name;
        public Type returnType;
        public ParameterInfo[] parameters;

        public MethodData(string name, Type returnType, ParameterInfo[] parameters)
        {
            this.name = name;
            this.returnType = returnType;
            this.parameters = parameters;
        }
    }
}