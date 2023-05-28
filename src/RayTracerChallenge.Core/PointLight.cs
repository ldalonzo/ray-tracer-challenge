namespace RayTracerChallenge.Core;

public record PointLight(Vector4 Position, Vector3 Intensity)
{
    public Vector4 Position { get; } = Position;

    public Vector3 Intensity { get; } = Intensity;
}
