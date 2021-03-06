using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Starter : IStartHook
    {
        // Output
        public event Action start;

        [ExcludeFromGraph]
        public void Start()
        {
            start?.Invoke();
        }
    }
}
