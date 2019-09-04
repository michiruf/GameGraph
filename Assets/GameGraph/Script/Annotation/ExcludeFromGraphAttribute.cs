using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event |
                    AttributeTargets.Method)]
    public sealed class ExcludeFromGraphAttribute : Attribute
    {
    }
}
