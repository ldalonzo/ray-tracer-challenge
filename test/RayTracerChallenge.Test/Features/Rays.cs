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
}
