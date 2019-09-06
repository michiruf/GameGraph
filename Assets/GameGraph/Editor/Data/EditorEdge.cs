using System;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorEdge
    {
        [SerializeField] private string inputNodeIdInternal;
        [SerializeField] private string inputPortIdInternal;
        [SerializeField] private string outputNodeIdInternal;
        [SerializeField] private string outputPortIdInternal;
        [NonSerialized] public bool isDirty;

        public string inputNodeId
        {
            get => inputNodeIdInternal;
            set
            {
                if (!string.Equals(value, inputNodeIdInternal)) MarkDirty();
                inputNodeIdInternal = value;
            }
        }

        public string inputPortId
        {
            get => inputPortIdInternal;
            set
            {
                if (!string.Equals(value, inputPortIdInternal)) MarkDirty();
                inputPortIdInternal = value;
            }
        }

        public string outputNodeId
        {
            get => outputNodeIdInternal;
            set
            {
                if (!string.Equals(value, outputNodeIdInternal)) MarkDirty();
                outputNodeIdInternal = value;
            }
        }

        public string outputPortId
        {
            get => outputPortIdInternal;
            set
            {
                if (!string.Equals(value, outputPortIdInternal)) MarkDirty();
                outputPortIdInternal = value;
            }
        }

        private void MarkDirty()
        {
            isDirty = true;
        }
    }
}
