using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class ButtonControl<TValue> : Control
    {
        protected readonly string fieldName;
        protected readonly EditorNode node;
        protected readonly Button button;
        protected TValue value;

        public ButtonControl(string fieldName, string label, EditorNode node, string buttonText = null)
        {
            this.fieldName = fieldName;
            this.node = node;

            Add(new Label(label));
            button = new Button {text = buttonText ?? label};
            Add(button);
        }

        protected TGetValueType GetValueAs<TGetValueType>()
        {
            if (node.propertyValues.ContainsKey(fieldName))
            {
                var valueObject = node.propertyValues[fieldName];
                if (valueObject != null)
                    return (TGetValueType) valueObject;
            }

            return default;
        }

        protected TValue GetValue()
        {
            return GetValueAs<TValue>();
        }

        protected void LoadValue()
        {
            value = GetValue();
        }

        public override void PersistState()
        {
            if (value != null)
                node.propertyValues[fieldName] = value;
        }

        public override void RemoveState()
        {
            // Nothing to do here since the state gets cleared in the NodeView before collecting all
            // See NodeView.PersistState for the delegating call
        }
    }
}
