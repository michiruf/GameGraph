using System;
using System.Reflection;
using UnityEngine;

namespace GameGraph.Editor
{
    // TODO Rename parameter to references everywhere?

    [Serializable]
    public class EditorParameter : ISerializationCallbackReceiver
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

        private Type typeValue;
        public Type type
        {
            get => typeValue;
            set
            {
                if (value == typeValue) return;
                typeValue = value;
                // NOTE Reflection might not belong here. Create a type wrapper maybe?
                isGameGraphTypeInternal = value.GetCustomAttribute<GameGraphAttribute>() != null;
                MarkDirty();
            }
        }

        public bool isGameGraphType => isGameGraphTypeInternal;

        public EditorParameter(string name, Type type)
        {
            this.name = name;
            this.type = type;
            MarkDirty();
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        public void OnBeforeSerialize()
        {
            typeInternal = typeValue.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            typeValue = typeInternal.ToType();
        }
    }
}
