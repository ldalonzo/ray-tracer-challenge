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
    /// Add together the material's ambient, diffuse, and specular components, weighted by the angles between the different vectors.
    /// </summary>
    /// <param name="light">The light source</param>
    /// <param name="point">The point being illuminated.</param>
    /// <param name="eyeV">The eye vector from the Phong reflection model.</param>
    /// <param name="normalV">The normal vectors from the Phong reflection model.</param>
    public Vector3 Lighting(PointLight light, Vector4 point, Vector4 eyeV, Vector4 normalV)
    {
        var (ambient, diffuse, specular) = Lighting3(light, point, eyeV, normalV);
        return ambient + diffuse + specular;
    }

    public (Vector3, Vector3, Vector3) Lighting3(PointLight light, Vector4 point, Vector4 eyeV, Vector4 normalV)
    {
        var effectiveColor = Color * light.Intensity;

        // Find the direction to the light source.
        var lightV = Vector4.Normalize(light.Position - point);

        // Compute the ambient contribution
        var ambient = effectiveColor * Ambient;

        Vector3 diffuse;
        Vector3 specular;

        // Compute the cosine of the angle between the light vector and the normal vector.
        var lightDotNormal = Vector4.Dot(lightV, normalV);
        if (lightDotNormal < 0)
        {
            // A negative number means the light is on the other side of the surface.
            diffuse = Vector3.Zero;
            specular = Vector3.Zero;
        }
        else
        {
            // Compute the diffuse contribution.
            diffuse = effectiveColor * Diffuse * lightDotNormal;

            // Compute the cosine of the angle between the reflection vector and the eye vector.
            var reflectv = -lightV.Reflect(normalV);
            var reflectDotEye = Vector4.Dot(reflectv, eyeV);
            if (reflectDotEye <= 0)
            {
                // A negative number means the light reflects away from the eye.
                specular = Vector3.Zero;
            }
            else
            {
                // Compute the specular contribution.
                var factor = MathF.Pow(reflectDotEye, Shininess);
                specular = light.Intensity * Specular * factor;
            }
        }

        return (ambient, diffuse, specular);
    }
}
