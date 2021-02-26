using System;

namespace GameGraph
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GameGraphAttribute : Attribute
    {
        // NOTE Handle modes of blacklist and whitelist to be able to control behaviour as wanted
        // TODO Implement this customEditorView

        public readonly string group;
        public readonly Type customEditorView;

        public GameGraphAttribute()
        {
        }

        public GameGraphAttribute(string group)
        {
            this.group = group;
        }

        public GameGraphAttribute(string group, Type customEditorView) : this(group)
        {
            this.customEditorView = customEditorView;
        }
    }
}
