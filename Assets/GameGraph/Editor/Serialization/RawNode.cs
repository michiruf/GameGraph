using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawNode
    {
        [SerializeField] private string idInternal;
        [SerializeField] private string nameInternal;
        [SerializeField] private Vector2 positionInternal;
        [NonSerialized] public bool isDirty;

        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(idInternal))
                    idInternal = GUID.Generate().ToString();
                return idInternal;
            }
        }

        public string name => nameInternal;

        public Vector2 position
        {
            get => positionInternal;
            set
            {
                if (!value.Equals(positionInternal)) MarkDirty();
                positionInternal = value;
            }
        }

        public RawNode(string name)
        {
            nameInternal = name;
        }

        private void MarkDirty()
        {
            isDirty = true;
        }
    }
}
