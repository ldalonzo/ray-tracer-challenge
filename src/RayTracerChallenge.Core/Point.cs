namespace RayTracerChallenge.Core;

public static class Point
{
    public static Vector4 Create(float x, float y, float z) => new(x, y, z, 1);
}
