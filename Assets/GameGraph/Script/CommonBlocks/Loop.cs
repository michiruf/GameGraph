using System;
using JetBrains.Annotations;

namespace GameGraph
{
    [GameGraph]
    [UsedImplicitly]
    public class Loop
    {
        public int count;

        public event Action execute;

        public void Invoke()
        {
            for (var i = 0; i < count; i++)
            {
                execute?.Invoke();
            }
        }
    }
}
