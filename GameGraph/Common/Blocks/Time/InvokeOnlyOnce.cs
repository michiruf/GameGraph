using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Time")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class InvokeOnlyOnce
    {
        // Output
        public event Action @out;

        private bool invoked;

        public void Invoke()
        {
            if (!invoked)
                @out?.Invoke();

            invoked = true;
        }
    }
}
