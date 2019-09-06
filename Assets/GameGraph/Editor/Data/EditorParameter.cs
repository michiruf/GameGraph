using System;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorParameter
    {
        [SerializeField] private string idInternal;
        [SerializeField] private string nameInternal;
        [SerializeField] private string typeNameInternal;
        [SerializeField] private string typeAssemblyQualifiedNameInternal;
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

        public string name
        {
            get => nameInternal;
            set
            {
                if (!string.Equals(value, nameInternal)) MarkDirty();
                nameInternal = value;
            }
        }

        public string typeName => typeNameInternal.PrettifyName();

        public string typeAssemblyQualifiedName => typeAssemblyQualifiedNameInternal;

        public EditorParameter(TypeData data)
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
