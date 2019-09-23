using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorNode : ISerializationCallbackReceiver
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private SerializableType typeInternal;
        [SerializeField] private string parameterIdInternal;
        [SerializeField] private StringSerializableObjectDictionary propertyValuesInternal;
        [SerializeField] private Vector2 positionInternal;
        [NonSerialized] public bool isDirty;

        public string id => idInternal;

        public Type type => typeInternal;

        public string parameterId
        {
            get => parameterIdInternal;
            set
            {
                if (string.Equals(value, parameterIdInternal)) return;
                parameterIdInternal = value;
                MarkDirty();
            }
        }

        public Dictionary<string, object> propertyValues { get; private set; } = new Dictionary<string, object>();

        public Vector2 position
        {
            get => positionInternal;
            set
            {
                if (value.Equals(positionInternal)) return;
                positionInternal = value;
                MarkDirty();
            }
        }

        public EditorNode(SerializableType type)
        {
            typeInternal = type;
            propertyValuesInternal = new StringSerializableObjectDictionary();
            MarkDirty();
        }

        public EditorParameter GetParameter(EditorGameGraph graph)
        {
            return graph.parameters.FirstOrDefault(parameter => parameter.id.Equals(parameterId));
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        public void OnBeforeSerialize()
        {
            if (propertyValues != null)
                propertyValuesInternal.dictionary = propertyValues
                    .ToDictionary(pair => pair.Key, pair => new SerializableObject(pair.Value));
        }

        public void OnAfterDeserialize()
        {
            if (propertyValuesInternal != null)
                propertyValues = propertyValuesInternal.dictionary
                    .ToDictionary(pair => pair.Key, pair => pair.Value.@object);
        }
    }
}
