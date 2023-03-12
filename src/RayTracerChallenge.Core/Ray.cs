namespace RayTracerChallenge.Core;

public record struct Ray(Vector4 Origin, Vector4 Direction)
{
    public Vector4 Position(float t)
    {
        return Origin;
    }
}
