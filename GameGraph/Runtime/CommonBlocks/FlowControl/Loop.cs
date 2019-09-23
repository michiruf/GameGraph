using System;
using JetBrains.Annotations;

namespace GameGraph
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Loop
    {
        // Output
        public int currentIndex { get; private set; }

        // Properties
        public int count { private get; set; }

        public event Action Execute;

        public void Invoke()
        {
            for (var i = 0; i < count; i++)
            {
                currentIndex = i;
                Execute?.Invoke();
            }
        }
    }
}
