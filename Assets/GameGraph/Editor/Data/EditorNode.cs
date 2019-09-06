using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorNode : ISerializationCallbackReceiver
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private SerializableType typeInternal;
        [SerializeField] private string parameterIdInternal;
        [SerializeField] private Vector2 positionInternal;
        [NonSerialized] public bool isDirty;
        
        [NonSerialized] public NodeView owner; // TODO Use these owners?!

        public string id => idInternal;

        public Type type { get; private set; }

        public string parameterId
        {
            get => parameterIdInternal;
            set
            {
                if (!string.Equals(value, parameterIdInternal)) MarkDirty();
                parameterIdInternal = value;
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

        public EditorNode(Type type)
        {
            this.type = type;
            MarkDirty();
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        public void OnBeforeSerialize()
        {
            typeInternal = type.ToSerializable();
        }

        public void OnAfterDeserialize()
        {
            type = typeInternal.ToType();
        }
    }
}
