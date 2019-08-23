using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    internal static class UxmlTraitsExtension
    {
        public static UxmlStringAttributeDescription ToAttribute(this string name)
        {
            return new UxmlStringAttributeDescription
            {
                name = name
            };
        }
    }
}
