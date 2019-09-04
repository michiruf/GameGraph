using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InputOnlyAttribute : Attribute
    {
        // TODO Use this to define the behaviour of property ports
    }
}
