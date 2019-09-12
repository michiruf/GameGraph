using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event)]
    public sealed class RequiredAttribute : Attribute
    {
        // NOTE Implement this for class data (fields, properties & maybe methods) when validation exists
    }
}
