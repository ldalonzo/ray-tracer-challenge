using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Rays
{
    [Fact]
    public void Creating_and_querying_a_ray()
    {
        var origin = Primitives.Point(1, 2, 3);
        var direction = Primitives.Vector(4, 5, 6);
        var ray = new Ray(origin, direction);

        ray.Origin.Should().Be(origin);
        ray.Direction.Should().Be(direction);
    }

    [Theory]
    [InlineData(  0,   2, 3, 4)]
    [InlineData(  1,   3, 3, 4)]
    [InlineData( -1,   1, 3, 4)]
    [InlineData(2.5, 4.5, 3, 4)]
    public void Computing_a_point_from_a_distance(float t, float expX, float expY, float expZ)
    {
        var ray = new Ray(Primitives.Point(2, 3, 4), Primitives.Vector(1, 0, 0));

        var p = ray.Position(t);

        p.X.Should().Be(expX);
        p.Y.Should().Be(expY);
        p.Z.Should().Be(expZ);
        p.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Translating_a_ray()
    {
        var ray = new Ray(Primitives.Point(1, 2, 3), Primitives.Vector(0, 1, 0));
        var m = Matrix4x4.CreateTranslation(3, 4, 5);

        var r2 = ray.Transform(m);

        r2.Should().NotBeSameAs(ray);
        r2.Origin.Should().Be(Primitives.Point(4, 6, 8));
        r2.Direction.Should().Be(Primitives.Vector(0, 1, 0));
    }

    [Fact]
    public void Scaling_a_ray()
    {
        var ray = new Ray(Primitives.Point(1, 2, 3), Primitives.Vector(0, 1, 0));
        var m = Matrix4x4.CreateScale(2, 3, 4);

        var r2 = ray.Transform(m);

        r2.Should().NotBeSameAs(ray);
        r2.Origin.Should().Be(Primitives.Point(2, 6, 12));
        r2.Direction.Should().Be(Primitives.Vector(0, 3, 0));
    }
}
