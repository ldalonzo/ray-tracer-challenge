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
}
