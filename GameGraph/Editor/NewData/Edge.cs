using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor.NewData
{
    [Serializable]
    public class Edge : IDataVisual
    {
        [SerializeField] private NodePort outputInternal;
        [SerializeField] private NodePort inputInternal;

        public GraphData owner { get; set; }
        private VisualElement viewInternal;
        public VisualElement view => viewInternal ?? (viewInternal = InstantiateView());

        public NodePort output
        {
            get => outputInternal;
            set
            {
                if (Equals(value, outputInternal)) return;
                outputInternal = value;
                owner.owner.isDirty = true;
            }
        }

        public NodePort input
        {
            get => inputInternal;
            set
            {
                if (Equals(value, inputInternal)) return;
                inputInternal = value;
                owner.owner.isDirty = true;
            }
        }

        public VisualElement InstantiateView()
        {
            throw new NotImplementedException();
        }
    }
}
