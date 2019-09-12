using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GameGraphAttribute : Attribute
    {
        // NOTE Handle modes of blacklist and whitelist to be able to control behaviour as wanted
        
        public readonly string group;

        public GameGraphAttribute()
        {
        }

        public GameGraphAttribute(string group)
        {
            this.group = group;
        }
    }
}
