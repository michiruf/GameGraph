using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [UsedImplicitly]
    public class BlackboardNameField : BlackboardField
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<BlackboardNameField>
        {
        }
    }
}
