using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IfButton
    {
        // Output
        public event Action @true;
        public event Action @false;
        public bool @out => InputDetection.GetButton(button, inputDetectionType);

        // Properties
        public string button { private get; set; }
        public InputDetectionType inputDetectionType;

        public void Invoke()
        {
            if (@out)
                @true?.Invoke();
            else
                @false?.Invoke();
        }
    }
}
