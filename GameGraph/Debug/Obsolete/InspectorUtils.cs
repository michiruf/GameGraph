using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    public static class InspectorUtils
    {
        // Looking up properties is pretty weird. Regular lookup with FindPropertyRelative() does not work at all.
        // See https://answers.unity.com/questions/543010/odd-behavior-of-findpropertyrelative.html
        // for more information
        public static SerializedObject FindObjectForProperty(this SerializedObject o, string propertyPath)
        {
            return o.FindProperty(propertyPath).GetObject(o.context);
        }

        public static SerializedObject GetObject(this SerializedProperty property, Object context)
        {
            return property.objectReferenceValue != null
                ? new SerializedObject(property.objectReferenceValue, context)
                : null;
        }
    }
}
