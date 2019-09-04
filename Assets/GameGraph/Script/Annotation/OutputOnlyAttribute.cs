using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class OutputOnlyAttribute : Attribute
    {
        // TODO Use this to define the behaviour of property ports
    }
}
