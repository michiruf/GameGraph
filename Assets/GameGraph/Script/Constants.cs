using System.Reflection;

namespace GameGraph
{
    public static class GameGraphConstants
    {
        public const BindingFlags ReflectionFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
    }
}
