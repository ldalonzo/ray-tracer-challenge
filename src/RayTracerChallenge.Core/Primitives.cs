namespace RayTracerChallenge.Core;

public static class Primitives
{
    public static Vector4 Point(float x, float y, float z) => new(x, y, z, 1);

    public static Vector4 Vector(float x, float y, float z) => new(x, y, z, 0);
}
