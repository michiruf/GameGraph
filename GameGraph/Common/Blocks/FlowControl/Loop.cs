using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Loop
    {
        // Output
        public event Action execute;
        public int currentIndex { get; private set; }

        // Properties
        public int count { private get; set; }

        public void Invoke()
        {
            for (var i = 0; i < count; i++)
            {
                currentIndex = i;
                execute?.Invoke();
            }
        }
    }
}
