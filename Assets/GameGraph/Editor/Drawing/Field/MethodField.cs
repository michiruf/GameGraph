using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class MethodField : VisualElement
    {
        public MethodField(FieldData data, ValueEntry value)
        {
            Add(new Label(data.name));
            Add(new Label(data.type.Name));
            Add(new Label(value.value?.ToString() ?? "No value"));
        }
    }
}
