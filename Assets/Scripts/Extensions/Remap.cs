public static class Remap
{
    public static float DoRemap(float aMin, float aMax, float bMin, float bMax, float v)
    {
        float t = InvLerp(aMin, aMax, v);
        return Lerp(bMin, bMax, t);
    }

    private static float Lerp(float a, float b, float t)
    {
        return (1.0f - t) * a + b * t;
    }

    private static float InvLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }
}