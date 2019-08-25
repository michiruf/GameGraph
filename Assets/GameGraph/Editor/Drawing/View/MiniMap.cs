using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class MiniMap : UnityEditor.Experimental.GraphView.MiniMap
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<MiniMap>
        {
        }
    }
}
