using UnityEditor;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class DrawingEventHandlerExtension
    {
        public static void AddEventBus(this EditorWindow window)
        {
            window.rootVisualElement.AddUserData(new EventBus());
        }

        public static EventBus GetEventBus(this VisualElement element)
        {
            if (!element.HasUserData<EventBus>())
                element.AddUserData(element.GetRoot().GetUserData<EventBus>());
            return element.GetUserData<EventBus>();
        }
    }
}
