using UnityEditor;

namespace GameGraph.Editor
{
    // TODO Use this --> No, add GetWindow(this VisualElement e)
    public interface IWindowReceiver
    {
        EditorWindow window { get; set; }
    }
}
