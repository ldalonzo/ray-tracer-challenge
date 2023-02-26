using System.Numerics;

namespace RayTracerChallenge.Test;

public static class Vector
{
    public static Vector4 Create(float x, float y, float z) => new(x, y, z, 0);
}
