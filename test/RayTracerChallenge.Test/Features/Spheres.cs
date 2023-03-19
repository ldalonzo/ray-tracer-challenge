using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Spheres
{
    private const float Tolerance = 1E-5F;

    [Fact]
    public void A_ray_intersects_a_sphere_at_two_points()
    {
        var ray = new Ray(Primitives.Point(0, 0, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].T.Should().Be(4);
        xs[1].T.Should().Be(6);
    }

    [Fact]
    public void A_ray_intersects_a_sphere_at_a_tangent()
    {
        var ray = new Ray(Primitives.Point(0, 1, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].T.Should().Be(5);
        xs[1].T.Should().Be(5);
    }

    [Fact]
    public void A_ray_misses_a_sphere()
    {
        var ray = new Ray(Primitives.Point(0, 2, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray);

        xs.Should().BeEmpty();
    }

    [Fact]
    public void A_ray_originates_inside_a_sphere()
    {
        var ray = new Ray(Primitives.Point(0, 0, 0), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].T.Should().Be(-1.0F);
        xs[1].T.Should().Be(1.0F);
    }

    [Fact]
    public void A_sphere_is_behind_a_ray()
    {
        var ray = new Ray(Primitives.Point(0, 0, 5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].T.Should().Be(-6.0F);
        xs[1].T.Should().Be(-4.0F);
    }

    [Fact]
    public void A_sphere_s_default_transformation()
    {
        var s = new Sphere();

        s.Transform.Should().Be(Matrix4x4.Identity);
    }

    [Theory]
    [AutoData]
    public void Changing_a_sphere_s_transformation(float scaleX, float scaleY, float scaleZ)
    {
        var transform = Matrix4x4.CreateScale(scaleX, scaleY, scaleZ);

        var s = new Sphere() with { Transform = transform };

        s.Transform.Should().Be(transform);
    }

    [Fact]
    public void Intersecting_a_scaled_sphere_with_a_ray()
    {
        var ray = new Ray(Primitives.Point(0, 0, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere() with { Transform = Matrix4x4.CreateScale(2, 2, 2) };

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].T.Should().Be(3);
        xs[1].T.Should().Be(7);
    }

    [Fact]
    public void Intersecting_a_translated_sphere_with_a_ray()
    {
        var ray = new Ray(Primitives.Point(0, 0, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere() with { Transform = Matrix4x4.CreateTranslation(5, 0, 0) };

        var xs = s.Intersect(ray);

        xs.Should().BeEmpty();
    }

    [Fact]
    public void The_normal_on_a_sphere_at_a_point_on_the_x_axis()
    {
        var s = new Sphere();

        var normal = s.NormalAt(Primitives.Point(1, 0, 0));

        normal.IsVector().Should().BeTrue();
        normal.X.Should().Be(1);
        normal.Y.Should().Be(0);
        normal.Z.Should().Be(0);
    }

    [Fact]
    public void The_normal_on_a_sphere_at_a_point_on_the_y_axis()
    {
        var s = new Sphere();

        var normal = s.NormalAt(Primitives.Point(0, 1, 0));

        normal.IsVector().Should().BeTrue();
        normal.X.Should().Be(0);
        normal.Y.Should().Be(1);
        normal.Z.Should().Be(0);
    }

    [Fact]
    public void The_normal_on_a_sphere_at_a_point_on_the_z_axis()
    {
        var s = new Sphere();

        var normal = s.NormalAt(Primitives.Point(0, 0, 1));

        normal.IsVector().Should().BeTrue();
        normal.X.Should().Be(0);
        normal.Y.Should().Be(0);
        normal.Z.Should().Be(1);
    }

    [Fact]
    public void The_normal_on_a_sphere_at_a_nonaxial_point()
    {
        var s = new Sphere();

        var normal = s.NormalAt(Primitives.Point(MathF.Sqrt(3) / 3F, MathF.Sqrt(3) / 3F, MathF.Sqrt(3) / 3F));

        normal.IsVector().Should().BeTrue();
        normal.X.Should().BeApproximately(MathF.Sqrt(3) / 3F, Tolerance);
        normal.Y.Should().BeApproximately(MathF.Sqrt(3) / 3F, Tolerance);
        normal.Z.Should().BeApproximately(MathF.Sqrt(3) / 3F, Tolerance);
    }

    [Fact]
    public void The_normal_is_a_normalized_vector()
    {
        var s = new Sphere();

        var normal = s.NormalAt(Primitives.Point(MathF.Sqrt(3) / 3F, MathF.Sqrt(3) / 3F, MathF.Sqrt(3) / 3F));

        normal.Length().Should().BeApproximately(1f, Tolerance);
    }

    [Fact]
    public void Computing_the_normal_on_a_translated_sphere()
    {
        var s = new Sphere { Transform = Matrix4x4.CreateTranslation(0, 1, 0) };

        var normal = s.NormalAt(Primitives.Point(0F, 1.70711F, -0.70711F));

        normal.X.Should().BeApproximately(0, Tolerance);
        normal.Y.Should().BeApproximately(0.70711F, Tolerance);
        normal.Z.Should().BeApproximately(-0.70711f, Tolerance);
    }
}
