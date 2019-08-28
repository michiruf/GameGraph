using System;
using GameGraph.Annotation;
using JetBrains.Annotations;
using Property = GameGraph.Annotation.PropertyAttribute;

namespace GameGraph.CommonBlocks
{
    [GameGraph]
    [UsedImplicitly]
    public class Loop
    {
        [Annotation.Property] // 
        public int count;

        [Trigger] //
        public Action execute;

        [Method] //
        public void Invoke()
        {
            for (var i = 0; i < count; i++)
            {
                execute?.Invoke();
            }
        }
    }
}
