using System;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class ControlFactory
    {
        public static VisualElement Create(string name, string fieldName, Type type, EditorNode node)
        {
//            if (type == typeof(int))
//                return new IntControl(fieldName, node);
            if (type == typeof(float))
                return new FloatControl(fieldName, name, node);
            if (type == typeof(string))
                return new StringControl(fieldName, name, node);
//            if (type == typeof(Vector2))
//                return new ControlBase<,,>(fieldName, node);
//            if (type == typeof(Vector3))
//                return new ControlBase<,,>(fieldName, node);
//            if (type == typeof(Type))
//                return new ControlView<Type>(new TypeField(name), node);

            // TODO More fields

            return null;
        }
    }
}
