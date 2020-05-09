using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Type
    {
        // TODO Type is not yet working properly with serialization...
        //      And the concept of this is pretty bad like this
        //      See also TypeButtonControl's magic constants
        public System.Type type
        {
            get => System.Type.GetType(typeInternal);
            set => typeInternal = value.AssemblyQualifiedName;
        }
        [ExcludeFromGraph]
        public string typeInternal;
    }
}
