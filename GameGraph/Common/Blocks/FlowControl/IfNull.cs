using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IfNull
    {
        // Output
        public bool result => @object == null;
        public event Action @true;
        public event Action @false;

        // Properties
        public object @object { get; set; }

        public void Invoke()
        {
            if (result)
                @true?.Invoke();
            else
                @false?.Invoke();
        }
    }
}
