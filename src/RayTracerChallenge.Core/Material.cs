namespace RayTracerChallenge.Core;

public record Material(Vector3 Color, float Ambient, float Diffuse, float Specular, float Shininess)
{
    public static readonly Material Default = new(Core.Color.Create(1F, 1F, 1F), 0.1F, 0.9F, 0.9F, 200F);

    public Vector3 Color { get; init; } = Color;

    public float Ambient { get; init; } = Ambient;

    public float Diffuse { get; init; } = Diffuse;

    public float Specular { get; init; } = Specular;

    public float Shininess { get; init; } = Shininess;
}
