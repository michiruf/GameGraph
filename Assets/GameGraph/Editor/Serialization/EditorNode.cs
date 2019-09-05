using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorNode
    {
        [SerializeField] private string idInternal;
        [SerializeField] private string nameInternal;
        [SerializeField] private string typeAssemblyQualifiedNameInternal;
        public bool instanceNameActive;
        [SerializeField] private string instanceNameInternal;
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

        public string name => nameInternal.PrettifyName();

        public string typeAssemblyQualifiedName => typeAssemblyQualifiedNameInternal;

        public string instanceName
        {
            get => instanceNameInternal;
            set
            {
                if (!string.Equals(value, instanceNameInternal)) MarkDirty();
                instanceNameInternal = value;
            }
        }

        public Vector2 position
        {
            get => positionInternal;
            set
            {
                if (!value.Equals(positionInternal)) MarkDirty();
                positionInternal = value;
            }
        }

        public EditorNode(TypeData data)
        {
            nameInternal = data.name;
            typeAssemblyQualifiedNameInternal = data.assemblyQualifiedName;
            isDirty = true;
        }

        private void MarkDirty()
        {
            isDirty = true;
        }
    }
}
