namespace RayTracerChallenge.Core;

public record Sphere : ISceneObject
{
    public Vector4 Center { get; } = Primitives.Point(0, 0, 0);

    public Material Material { get; init; } = Material.Default;

    private readonly Matrix4x4 _transform = Matrix4x4.Identity;
    private readonly Matrix4x4 _transformInverse = Matrix4x4.Identity;

    /// <summary>
    /// Transforms points from object space to world space.
    /// </summary>
    public Matrix4x4 Transform
    {
        get => _transform;
        init
        {
            _transform = value;
            Matrix4x4.Invert(Transform, out _transformInverse);
        }
    }

    public IEnumerable<Intersection> Intersect(Ray worldRay)
    {
        var objectRay = worldRay.Transform(_transformInverse);

        var sphereToRay = objectRay.Origin - Center;

        var a = Vector4.Dot(objectRay.Direction, objectRay.Direction);
        var b = 2 * Vector4.Dot(objectRay.Direction, sphereToRay);
        var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;

        var discriminant = MathF.Pow(b, 2) - 4 * a * c;
        if (discriminant >= 0)
        {
            var ds = MathF.Sqrt(discriminant);

            yield return new Intersection(this, (-b - ds) / (2 * a));
            yield return new Intersection(this, (-b + ds) / (2 * a));
        }
    }

    public Vector4 NormalAt(Vector4 worldPoint)
    {
        var objectPoint = Vector4.Transform(worldPoint, _transformInverse);
        var objectNormal = objectPoint - Center;

        var worldNormal = Vector4.Transform(objectNormal, Matrix4x4.Transpose(_transformInverse));
        worldNormal.W = 0;

        return Vector4.Normalize(worldNormal);
    }
}
