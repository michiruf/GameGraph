using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GameGraphAttribute : Attribute
    {
        // TODO Add menu name when a context menu exists!
        // TODO Add boolean to handle either a blacklist or a whitelist!
    }
}
