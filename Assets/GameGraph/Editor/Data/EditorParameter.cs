using System;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorParameter : ISerializationCallbackReceiver
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private string nameInternal;
        [SerializeField] private SerializableType typeInternal;
        [NonSerialized] public bool isDirty;

        [NonSerialized] public ParameterView owner; // TODO Use these owners?!

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

        private Type typeValue;
        public Type type
        {
            get => typeValue;
            set
            {
                if (value != typeValue) MarkDirty();
                typeValue = value;
            }
        }

        public EditorParameter(string name, Type type)
        {
            this.name = name;
            typeValue = type;
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
