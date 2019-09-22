using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class MiniMapEditorView : MiniMap
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<MiniMapEditorView>
        {
        }
    }
}
