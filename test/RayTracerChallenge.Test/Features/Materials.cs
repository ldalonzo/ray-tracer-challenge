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
}
