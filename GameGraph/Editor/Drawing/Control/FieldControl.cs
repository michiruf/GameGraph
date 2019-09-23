using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class FieldControl<TValue, TField> : ControlBase
        where TField : BaseField<TValue>, new()
    {
        private readonly string fieldName;
        private readonly string label;
        private readonly EditorNode node;
        private TValue value;

        public FieldControl(string fieldName, string label, EditorNode node)
        {
            this.fieldName = fieldName;
            this.label = label;
            this.node = node;
            Initialize();
        }

        private void Initialize()
        {
            var field = new TField {label = label};

            if (node.propertyValues.ContainsKey(fieldName))
            {
                var valueObject = node.propertyValues[fieldName];
                if (valueObject != null)
                    value = (TValue) valueObject;
            }

            if (value != null)
                field.SetValueWithoutNotify(value);

            field.RegisterValueChangedCallback(OnChange);
            Add(field);
        }

        private void OnChange(ChangeEvent<TValue> evt)
        {
            value = evt.newValue;
            SendEvent(new ControlValueChangeEvent());
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
