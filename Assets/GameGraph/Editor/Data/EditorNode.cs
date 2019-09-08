using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorNode
    {
        [SerializeField] private string idInternal = Guid.NewGuid().ToString();
        [SerializeField] private SerializableType typeInternal;
        [SerializeField] private string parameterIdInternal;
        [SerializeField] private Vector2 positionInternal;
        [NonSerialized] public bool isDirty;

        [NonSerialized] public NodeView owner; // TODO Use these owners?!

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
    }
}
