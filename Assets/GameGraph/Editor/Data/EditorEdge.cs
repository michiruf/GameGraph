using System;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorEdge
    {
        [SerializeField] private Connection outputConnection;
        [SerializeField] private Connection inputConnection;
        [NonSerialized] public bool isDirty;

        [NonSerialized] public EdgeView owner; // TODO Use these owners?!

        public string outputNodeId
        {
            get => outputConnection.nodeIdInternal;
            set
            {
                if (!string.Equals(value, outputConnection.nodeIdInternal)) MarkDirty();
                outputConnection.nodeIdInternal = value;
            }
        }

        public string outputPortName
        {
            get => outputConnection.portNameInternal;
            set
            {
                if (!string.Equals(value, outputConnection.portNameInternal)) MarkDirty();
                outputConnection.portNameInternal = value;
            }
        }

        public string inputNodeId
        {
            get => inputConnection.nodeIdInternal;
            set
            {
                if (!string.Equals(value, inputConnection.nodeIdInternal)) MarkDirty();
                inputConnection.nodeIdInternal = value;
            }
        }

        public string inputPortName
        {
            get => inputConnection.portNameInternal;
            set
            {
                if (!string.Equals(value, inputConnection.portNameInternal)) MarkDirty();
                inputConnection.portNameInternal = value;
            }
        }

        public EditorEdge()
        {
            MarkDirty();
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        [Serializable]
        private struct Connection
        {
            public string nodeIdInternal;
            public string portNameInternal;
        }
    }
}
