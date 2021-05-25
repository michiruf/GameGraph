using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class FindObjectsOfType
    {
        // Output
        public event Action found;
        public Object[] component { get; private set; }

        // Properties
        public GraphSerializableType type;

        public void Find()
        {
            component = Object.FindObjectsOfType(type.type);
            found?.Invoke();
        }
    }
}
