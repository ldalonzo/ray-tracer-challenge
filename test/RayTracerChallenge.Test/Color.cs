using System.Numerics;

namespace RayTracerChallenge.Test;

public struct Color
{
    public static Vector3 Create(float r, float g, float b) => new(r, g, b);
}
