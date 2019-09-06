using System;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorParameter
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private string nameInternal;
        [SerializeField] private string typeNameInternal;
        [SerializeField] private string typeAssemblyQualifiedNameInternal;
        [NonSerialized] public bool isDirty;

        public string id => idInternal;

        public string name
        {
            get => nameInternal;
            set
            {
                if (!string.Equals(value, nameInternal)) MarkDirty();
                nameInternal = value;
            }
        }

        public string typeName
        {
            get => typeNameInternal.PrettifyName();
            set
            {
                if (!string.Equals(value, typeNameInternal)) MarkDirty();
                typeNameInternal = value;
            }
        }

        public string typeAssemblyQualifiedName
        {
            get => typeAssemblyQualifiedNameInternal;
            set
            {
                if (!string.Equals(value, typeAssemblyQualifiedNameInternal)) MarkDirty();
                typeAssemblyQualifiedNameInternal = value;
            }
        }

        public EditorParameter(TypeData data)
        {
            name = "Parameter";
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
