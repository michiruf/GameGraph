using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetButton
    {
        // Output
        public bool @out => InputDetection.GetButton(button, inputDetectionType);

        // Properties
        public string button { private get; set; }
        public InputDetectionType inputDetectionType;
    }
}
