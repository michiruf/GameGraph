using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class FindObjectOfType
    {
        // Output
        public event Action fetched;
        public Object component { get; private set; }

        // Properties
        public GraphSerializableType type;

        public void Fetch()
        {
            component = Object.FindObjectOfType(type.type);
            fetched?.Invoke();
        }
    }
}
