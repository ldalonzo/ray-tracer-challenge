using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Spheres
{
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
}
