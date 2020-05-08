using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Type
    {
        // Output
        public System.Type type => System.Type.GetType(name);

        // Properties
        public string name { private get; set; }
    }
}
