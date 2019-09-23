using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class EditorGameGraph : ISerializationCallbackReceiver
    {
        [SerializeField] private int serializedVersion;
        public List<EditorParameter> parameters = new List<EditorParameter>();
        public List<EditorNode> nodes = new List<EditorNode>();
        public List<EditorEdge> edges = new List<EditorEdge>();
        // TODO Persist "window" config as well
        //      UserBounds: viewport GraphEditorView
        //      UserBounds: bounds ParameterEditorView
        //      UserBounds: bounds MiniMapEditorView
        private bool isDirtyInternal;
        
        public bool isDirty
        {
            get
            {
                return isDirtyInternal ||
                       parameters.Aggregate(false, (b, parameter) => b || parameter.isDirty) ||
                       nodes.Aggregate(false, (b, node) => b || node.isDirty) ||
                       edges.Aggregate(false, (b, edge) => b || edge.isDirty);
            }
            set
            {
                isDirtyInternal = value;
                parameters.ForEach(parameter => parameter.isDirty = value);
                nodes.ForEach(node => node.isDirty = value);
                edges.ForEach(edge => edge.isDirty = value);
            }
        }

        public void OnBeforeSerialize()
        {
            serializedVersion++;
            isDirty = false;
        }

        public void OnAfterDeserialize()
        {
            isDirty = false;
        }

        // TODO For undo use a simple chained list (non-serialized)?!
        //      Since this is all serializable that should work pretty well!
        //public void RegisterCompleteObjectUndo(string actionName)
        //{
        //    Undo.RegisterCompleteObjectUndo(this, actionName);
        //    serializedVersion++;
        //    isDirtyInternal = true;
        //}
    }
}
