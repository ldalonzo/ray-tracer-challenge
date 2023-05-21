namespace RayTracerChallenge.Core;

public record Material(Vector3 Color, float Ambient, float Diffuse, float Specular, float Shininess)
{
    public static readonly Material Default = new(Core.Color.Create(1F, 1F, 1F), 0.1F, 0.9F, 0.9F, 200F);

    public Vector3 Color { get; init; } = Color;

    /// <summary>
    /// Ambient reflection is background lighting, or light reflected from other objects in the environment.
    /// </summary>
    public float Ambient { get; init; } = Ambient;

    /// <summary>
    /// Diffuse reflection is light reflected from a matte surface. It depends only on the angle between the light source and the surface normal.
    /// </summary>
    public float Diffuse { get; init; } = Diffuse;

    /// <summary>
    /// Specular reflection is the reflection of the light source itself and results in what is called a specular highlight.
    /// </summary>
    public float Specular { get; init; } = Specular;

    public float Shininess { get; init; } = Shininess;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="light">The light source</param>
    /// <param name="point">The point being illuminated.</param>
    /// <param name="eyev">The eye vector from the Phong reflection model.</param>
    /// <param name="normalv">The normal vectors from the Phong reflection model.</param>
    /// <returns></returns>
    public Vector3 Lighting(PointLight light, Vector4 point, Vector4 eyev, Vector4 normalv)
    {
        var effectiveColor = Color * light.Intensity;

        // Compute the ambient contribution
        var ambient = effectiveColor * Ambient;

        // Compute the diffuse contribution.
        var diffuse = effectiveColor * Diffuse;

        // Compute the specular contribution.
        var specular = light.Intensity * Specular;

        return ambient + diffuse + specular;
    }
}
