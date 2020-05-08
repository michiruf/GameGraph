using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class If
    {
        // Output
        public event Action @true;
        public event Action @false;

        // Properties
        public bool condition { private get; set; }

        public void Invoke()
        {
            if (condition)
                @true?.Invoke();
            else
                @false?.Invoke();
        }
    }
}
