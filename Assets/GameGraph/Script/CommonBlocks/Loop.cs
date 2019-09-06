using System;
using JetBrains.Annotations;

namespace GameGraph
{
    [GameGraph]
    [UsedImplicitly]
    public class Loop
    {
        [UsedImplicitly] public int count;

        [UsedImplicitly]
        public int index { get; private set; }

        [UsedImplicitly]
        public event Action execute;

        [UsedImplicitly]
        public void Invoke()
        {
            for (var i = 0; i < count; i++)
            {
                index = i;
                execute?.Invoke();
            }
        }
    }
}
