using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawNode
    {
        public string id;
        public string name;
        public Vector2 position;

        public RawNode()
        {
            id = GUID.Generate().ToString();
        }

        public RawNode(string name) : this()
        {
            this.name = name;
        }

        public RawNode(string name, Vector2 position) : this(name)
        {
            this.position = position;
        }
    }
}
