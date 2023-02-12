using System.Numerics;

namespace RayTracerChallenge.Test;

public static class Vector4Extensions
{
    public static bool IsPoint(this Vector4 vector)
        => Math.Abs(vector.W - 1.0f) < float.Epsilon;

    public static bool IsVector(this Vector4 vector)
        => Math.Abs(vector.W) < float.Epsilon;
}
