namespace RayTracerChallenge.Test.Features;

public class Materials
{
    [Fact]
    public void The_default_material()
    {
        var m = Material.Default;

        m.Color.Should().Be(Color.Create(1F, 1F, 1F));
        m.Ambient.Should().Be(0.1F);
        m.Diffuse.Should().Be(0.9F);
        m.Specular.Should().Be(0.9F);
        m.Shininess.Should().Be(200F);
    }

    [Fact]
    public void Lighting_with_the_eye_between_the_light_and_the_surface()
    {
        var m = Material.Default;
        var position = Primitives.Point(0, 0, 0);
        var eyev = Primitives.Vector(0, 0, -1);
        var normalv = Primitives.Vector(0, 0, -1);
        var light = new PointLight(Primitives.Point(0, 0, -10), Color.Create(1F, 1F, 1F));

        var result = m.Lighting(light, position, eyev, normalv);

        // Expect ambient, diffuse, and specular to all be at full strength.
        result.Should().Be(Color.Create(1.9F, 1.9F, 1.9F));
    }
}
