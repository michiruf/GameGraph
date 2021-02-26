using System;

namespace GameGraph
{
    [Serializable]
    public class Parameter
    {
        public string name;
        public SerializableType type;

        public Parameter(string name, SerializableType type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
