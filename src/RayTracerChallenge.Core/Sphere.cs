namespace RayTracerChallenge.Core;

public record struct Sphere() : ISceneObject
{
    public Vector4 Center { get; } = Primitives.Point(0, 0, 0);

    private readonly Matrix4x4 _transform = Matrix4x4.Identity;
    private readonly Matrix4x4 _transformInverse = Matrix4x4.Identity;

    public Matrix4x4 Transform
    {
        get => _transform;
        init
        {
            _transform = value;
            Matrix4x4.Invert(Transform, out _transformInverse);
        }
    }

    public IEnumerable<Intersection> Intersect(Ray ray)
    {
        var ray2 = ray.Transform(_transformInverse);

        var sphereToRay = ray2.Origin - Center;

        var a = Vector4.Dot(ray2.Direction, ray2.Direction);
        var b = 2 * Vector4.Dot(ray2.Direction, sphereToRay);
        var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;

        var discriminant = MathF.Pow(b, 2) - 4 * a * c;
        if (discriminant >= 0)
        {
            var ds = MathF.Sqrt(discriminant);

            yield return new Intersection(this, (-b - ds) / (2 * a));
            yield return new Intersection(this, (-b + ds) / (2 * a));
        }
    }
}
