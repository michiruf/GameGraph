using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // ReSharper disable once UnusedMember.Global
    public class StateEditorView : VisualElement, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        // Used by Unity magically
        // ReSharper disable once UnusedMember.Global
        public new class UxmlFactory : UxmlFactory<StateEditorView>
        {
        }
    }
}
