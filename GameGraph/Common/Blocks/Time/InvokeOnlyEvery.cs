using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Time")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class InvokeOnlyEvery
    {
        // Output
        public event Action @out;

        // Properties
        public int count;

        private int current;

        public void Invoke()
        {
            current++;

            if (current % count == 0)
            {
                @out?.Invoke();
                // Reset to avoid overflows
                current = 0;
            }
        }
    }
}
