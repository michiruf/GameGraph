using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawNode
    {
        private string idInternal;
        public string name;
        public Vector2 position;
        public bool isDirty;

        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(idInternal))
                    idInternal = GUID.Generate().ToString();
                return idInternal;
            }
        }

        public RawNode(string name)
        {
            this.name = name;
        }

        public RawNode(string name, Vector2 position) : this(name)
        {
            this.position = position;
        }
    }
}
