namespace RayTracerChallenge.Core;

public record struct Sphere()
{
    public Vector4 Center { get; } = Primitives.Point(0, 0, 0);

    public IEnumerable<float> Intersect(Ray ray)
    {
        var sphereToRay = ray.Origin - Center;

        var a = Vector4.Dot(ray.Direction, ray.Direction);
        var b = 2 * Vector4.Dot(ray.Direction, sphereToRay);
        var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;

        var discriminant = MathF.Pow(b, 2) - 4 * a * c;
        if (discriminant >= 0)
        {
            var ds = MathF.Sqrt(discriminant);

            yield return (-b - ds) / 2 * a;
            yield return (-b + ds) / 2 * a;
        }
    }
}
