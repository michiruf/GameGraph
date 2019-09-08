using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph.Editor
{
    // TODO Rename parameter to references everywhere?

    [Serializable]
    public class EditorParameter
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private string nameInternal;
        [SerializeField] private SerializableType typeInternal;
        [SerializeField] private bool isGameGraphTypeInternal;
        [NonSerialized] public bool isDirty;

        [NonSerialized] public ParameterView owner; // TODO Use these owners?!

        public string id => idInternal;

        public string name
        {
            get => nameInternal;
            set
            {
                if (string.Equals(value, nameInternal)) return;
                nameInternal = value;
                MarkDirty();
            }
        }

        public Type type
        {
            get => typeInternal;
            set
            {
                if (value == typeInternal.type) return;
                typeInternal = value;
                // NOTE Reflection might not belong here
                isGameGraphTypeInternal = value.GetCustomAttribute<GameGraphAttribute>() != null;
                MarkDirty();
            }
        }

        public bool isGameGraphType => isGameGraphTypeInternal;

        public EditorParameter(string name, SerializableType type)
        {
            nameInternal = name;
            typeInternal = type;
            MarkDirty();
        }

        private void MarkDirty()
        {
            isDirty = true;
        }
    }
}
