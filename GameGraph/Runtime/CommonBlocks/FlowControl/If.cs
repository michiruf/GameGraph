using System;
using JetBrains.Annotations;

namespace GameGraph
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class If
    {
        // Properties
        public bool condition { private get; set; }

        public event Action Execute;

        public void Invoke()
        {
            if (condition)
                Execute?.Invoke();
        }
    }
}
