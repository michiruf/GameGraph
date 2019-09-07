using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class PropertyView : VisualElement
    {
        private PropertyView()
        {
            this.AddLayout(EditorConstants.ResourcesUxmlViewPath + "/PropertyView.uxml");
        }

        public PropertyView(FieldInfo info) : this()
        {
            Initialize(info.Name, info.FieldType, true, true);
        }

        public PropertyView(PropertyInfo info) : this()
        {
            Initialize(info.Name, info.PropertyType,
                info.SetMethod?.IsPublic ?? false,
                info.GetMethod?.IsPublic ?? false);
        }

        private void Initialize(string name, Type type, bool hasInput, bool hasOutput)
        {
            this.Q<Label>("name").text = name.PrettifyName();
            this.Q<Label>("value").text = "TODO";

            if (hasInput)
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    type,
                    name,
                    null,
                    type.Name);
                var container = this.Q<VisualElement>("ingoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }

            if (hasOutput)
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Output,
                    Port.Capacity.Single,
                    type,
                    name,
                    null,
                    type.Name);
                var container = this.Q<VisualElement>("outgoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
