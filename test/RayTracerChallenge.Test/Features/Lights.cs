namespace RayTracerChallenge.Test.Features;

public class Lights
{
    [Fact]
    public void A_point_light_has_a_position_and_intensity()
    {
        var intensity = Color.Create(1F, 1F, 1F);
        var position = Primitives.Point(0, 0, 0);

        var light = new PointLight(position, intensity);

        light.Position.Should().Be(position);
        light.Intensity.Should().Be(intensity);
    }
}
