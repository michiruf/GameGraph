using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class NodeViewInstanceNameContainer : VisualElement
    {
        private const string ContainerName = "instanceNameContainer";
        private const string TextFieldName = "instanceName";
        private const string ToggleName = "instanceNameToggle";

        private EditorNode node;

        private VisualElement containerInternal;
        public VisualElement container
        {
            get
            {
                if (containerInternal == null)
                    containerInternal = this.Q<VisualElement>(ContainerName);
                return containerInternal;
            }
        }

        private TextField textFieldInternal;
        public TextField textField
        {
            get
            {
                if (textFieldInternal == null)
                    textFieldInternal = this.Q<TextField>(TextFieldName);
                return textFieldInternal;
            }
        }

        public NodeViewInstanceNameContainer()
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/NodeViewInstanceNameContainer.uxml");
        }

        public void Initialize(EditorNode node)
        {
            this.node = node;
            textField.RegisterValueChangedCallback(evt => node.instanceName = evt.newValue);
        }

        public Toggle CreatToggle()
        {
            var toggle = new Toggle {name = ToggleName};
            toggle.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue)
                    container.RemoveFromClassList("hidden");
                else
                {
                    node.instanceName = null;
                    container.AddToClassList("hidden");
                }
                node.instanceNameActive = evt.newValue;
            });
            if (node.instanceNameActive)
                container.RemoveFromClassList("hidden");
            else
                container.AddToClassList("hidden");
            toggle.SetValueWithoutNotify(node.instanceNameActive);

            return toggle;
        }
    }
}
