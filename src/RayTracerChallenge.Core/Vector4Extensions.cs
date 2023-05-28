namespace RayTracerChallenge.Core;

public static class Vector4Extensions
{
    public static bool IsPoint(this Vector4 vector)
        => Math.Abs(vector.W - 1.0f) < float.Epsilon;

    public static bool IsVector(this Vector4 vector)
        => Math.Abs(vector.W) < float.Epsilon;

    public static Vector4 Reflect(this Vector4 vector, Vector4 normal)
    {
        return vector - normal * 2 * Vector4.Dot(vector, normal);
    }
}
