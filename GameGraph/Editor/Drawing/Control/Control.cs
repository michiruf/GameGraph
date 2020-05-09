using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public abstract class Control : VisualElement, IGraphElement
    {
        public EditorGameGraph graph { get; set; }

        public abstract void PersistState();

        public abstract void RemoveState();
    }
}
