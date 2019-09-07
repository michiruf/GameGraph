using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph
{
    [Serializable]
    public class Parameter : ISerializationCallbackReceiver
    {
        public string name;
        [SerializeField] private SerializableType serializableType;

        public Type type { get; private set; }
        public Object instance { get; set; }

        public Parameter(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public void FetchObjects()
        {
            // TODO Receive the objects anyhow from the inspector
        }

        public void OnBeforeSerialize()
        {
            serializableType = type.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            type = serializableType.ToType();
        }
    }
}
