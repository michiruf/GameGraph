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
                if (string.Equals(value, outputConnection.nodeIdInternal)) return;
                outputConnection.nodeIdInternal = value;
                MarkDirty();
            }
        }

        public string outputPortName
        {
            get => outputConnection.portNameInternal;
            set
            {
                if (string.Equals(value, outputConnection.portNameInternal)) return;
                outputConnection.portNameInternal = value;
                MarkDirty();
            }
        }

        public string inputNodeId
        {
            get => inputConnection.nodeIdInternal;
            set
            {
                if (string.Equals(value, inputConnection.nodeIdInternal)) return;
                inputConnection.nodeIdInternal = value;
                MarkDirty();
            }
        }

        public string inputPortName
        {
            get => inputConnection.portNameInternal;
            set
            {
                if (string.Equals(value, inputConnection.portNameInternal)) return;
                inputConnection.portNameInternal = value;
                MarkDirty();
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
