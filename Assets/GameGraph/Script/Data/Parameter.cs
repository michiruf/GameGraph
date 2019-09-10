using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace GameGraph
{
    [Serializable]
    public class Parameter
    {
        [UsedImplicitly] public string name;
        [UsedImplicitly] public SerializableType type;

        // TODO This does not belong here
        public Object instance; // TODO UnityEngine.Object or object?

        public Parameter(string name, SerializableType type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
