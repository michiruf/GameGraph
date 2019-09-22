using UnityEditor.UIElements;

namespace GameGraph.Editor
{
    public class FloatControl : ControlBase<float, double, FloatField>
    {
        public FloatControl(string fieldName, string label, EditorNode node) : base(fieldName, label, node)
        {
        }

        protected override float GetValue(double value)
        {
            return (float) value;
        }
    }
}
