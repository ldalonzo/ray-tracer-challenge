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
        xs[0].Should().Be(4);
        xs[1].Should().Be(6);
    }

    [Fact]
    public void A_ray_intersects_a_sphere_at_a_tangent()
    {
        var ray = new Ray(Primitives.Point(0, 1, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].Should().Be(5);
        xs[1].Should().Be(5);
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
        xs[0].Should().Be(-1.0F);
        xs[1].Should().Be(1.0F);
    }

    [Fact]
    public void A_sphere_is_behind_a_ray()
    {
        var ray = new Ray(Primitives.Point(0, 0, 5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray).ToList();

        xs.Should().HaveCount(2);
        xs[0].Should().Be(-6.0F);
        xs[1].Should().Be(-4.0F);
    }
}
