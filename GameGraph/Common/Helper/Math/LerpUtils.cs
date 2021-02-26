namespace GameGraph.Common.Helper
{
    public static class LerpUtils
    {
        public static float SmoothnessToLerp(float smoothness)
        {
            return 1f / (smoothness + 1f);
        }
    }
}
