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
        [SerializeField] private string typeNameInternal;
        [SerializeField] private string typeAssemblyQualifiedNameInternal;
        public bool isInstanced; // TODO Handle this anyhow (-> by id!)
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

        public string typeName => typeNameInternal.PrettifyName();

        public string typeAssemblyQualifiedName => typeAssemblyQualifiedNameInternal;

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
            typeNameInternal = data.name;
            typeAssemblyQualifiedNameInternal = data.assemblyQualifiedName;
            isDirty = true;
        }

        private void MarkDirty()
        {
            isDirty = true;
        }
    }
}
