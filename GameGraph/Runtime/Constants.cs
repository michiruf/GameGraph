using System.Reflection;

namespace GameGraph
{
    public static class Constants
    {
        public const BindingFlags ReflectionFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        public const float FloatingPrecision = 0.001f;
    }
}
