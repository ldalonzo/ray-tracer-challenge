namespace RayTracerChallenge.Core;

public class Transformation4x4Builder
{
    private readonly List<Matrix4x4> _transformations = new();

    public Transformation4x4Builder Append(Matrix4x4 transformation)
    {
        _transformations.Add(transformation);
        return this;
    }

    public Transformation4x4Builder WithShearing(float xy, float xz, float yx, float yz, float zx, float zy)
    {
        _transformations.Add(new Matrix4x4(
             1, yx, zx, 0,
            xy, 1f, zy, 0,
            xz, yz, 1f, 0,
             0, 0f, 0f, 1));

        return this;
    }

    public Matrix4x4 Build()
        => _transformations.Aggregate(Matrix4x4.Identity, Matrix4x4.Multiply);
}
