namespace RayTracerChallenge.Test.Features;

public class Intersections
{
    [Theory]
    [AutoData]
    public void An_intersection_encapsulates_t_and_object(float t)
    {
        var s = new Sphere();
        var i = new Intersection(s, t);

        i.T.Should().Be(t);
        i.Object.Should().Be(s);
    }

    [Fact]
    public void Intersect_sets_the_object_on_the_intersection()
    {
        var ray = new Ray(Primitives.Point(0, 0, -5), Primitives.Vector(0, 0, 1));
        var s = new Sphere();

        var xs = s.Intersect(ray);

        xs.Should().NotBeEmpty();
        xs.Should().AllSatisfy(i => i.Object.Should().Be(s));
    }

    [Fact]
    public void The_hit_when_all_intersections_have_positive_t()
    {
        var s = new Sphere();
        var i1 = new Intersection(s, 1);
        var i2 = new Intersection(s, 2);

        var i = new[] { i1, i2 }.Hit();

        i.Should().Be(i1);
    }

    [Fact]
    public void The_hit_when_some_intersections_have_negative_t()
    {
        var s = new Sphere();
        var i1 = new Intersection(s, -1);
        var i2 = new Intersection(s, 1);

        var i = new[] { i1, i2 }.Hit();

        i.Should().Be(i2);
    }

    [Fact]
    public void The_hit_when_all_intersections_have_negative_t()
    {
        var s = new Sphere();
        var i1 = new Intersection(s, -2);
        var i2 = new Intersection(s, -1);

        var i = new[] { i1, i2 }.Hit();

        i.Should().BeNull();
    }

    [Fact]
    public void The_hit_is_always_the_lowest_nonnegative_intersection()
    {
        var s = new Sphere();
        var i1 = new Intersection(s, 5);
        var i2 = new Intersection(s, 7);
        var i3 = new Intersection(s, -3);
        var i4 = new Intersection(s, 2);

        var i = new[] { i1, i2, i3, i4 }.Hit();

        i.Should().Be(i4);
    }
}
