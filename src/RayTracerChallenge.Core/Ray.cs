namespace RayTracerChallenge.Core;

public record struct Ray(Vector4 Origin, Vector4 Direction)
{
    public Vector4 Position(float t) => Origin + Direction * t;

    public Ray Transform(Matrix4x4 transform) => new(
        Vector4.Transform(Origin, transform),
        Vector4.Transform(Direction, transform));
}
