using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class FieldView : VisualElement
    {
        public FieldView(FieldData data, bool ingoingPort, bool outgoingPort, ValueEntry value)
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/FieldView.uxml");

            // Set simple data
            this.FindElementByName<Label>("name").text = data.name;
            this.FindElementByName<Label>("value").text = value.value?.ToString() ?? "VALUE NOT IMPLEMENTED";

            // Add ports
            if (ingoingPort)
            {
                var container = this.FindElementByName<VisualElement>("ingoingPortContainer");
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, data.type);
                container.Add(port);
                container.AddToClassList("exists");
            }
            if (outgoingPort)
            {
                var container = this.FindElementByName<VisualElement>("outgoingPortContainer");
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, data.type);
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
