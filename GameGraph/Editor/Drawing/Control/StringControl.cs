using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class StringControl : ControlBase<string, string, TextField>
    {
        public StringControl(string fieldName, string label, EditorNode node) : base(fieldName, label, node)
        {
        }

        protected override string GetValue(string value)
        {
            return value;
        }
    }
}
