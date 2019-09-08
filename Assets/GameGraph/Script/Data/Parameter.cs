using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph
{
    [Serializable]
    public class Parameter
    {
        public string name;
        public SerializableType type;

        public Object instance { get; set; }

        public Parameter(string name, SerializableType type)
        {
            this.name = name;
            this.type = type;
        }

        public void FetchObjects()
        {
            // TODO Receive the objects anyhow from the inspector
        }
    }
}
