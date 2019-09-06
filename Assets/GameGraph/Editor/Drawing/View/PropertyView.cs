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
            Initialize(data);
        }

        private void Initialize(MemberData<FieldInfo> data)
        {
            // Set simple data
            this.Q<Label>("name").text = data.name.PrettifyName();
            this.Q<Label>("value").text = "TODO";

            // Add ports
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    data.info.FieldType,
                    data.name,
                    null,
                    data.info.FieldType.Name);
                var container = this.Q<VisualElement>("ingoingPortContainer");
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
                    null,
                    data.info.FieldType.Name);
                var container = this.Q<VisualElement>("outgoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
