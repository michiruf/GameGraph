using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class PropertyView : VisualElement
    {
        public PropertyView(MemberData<FieldInfo> data)
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/PropertyView.uxml");

            // Set simple data
            this.FindElementByName<Label>("name").text = data.name.PrettifyName();
            this.FindElementByName<Label>("value").text = "TODO"; // TODO

            // Add ports
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    data.info.FieldType,
                    data.name,
                    false);
                var container = this.FindElementByName<VisualElement>("ingoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Output,
                    Port.Capacity.Single,
                    data.info.FieldType,
                    data.name,
                    false);
                var container = this.FindElementByName<VisualElement>("outgoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
