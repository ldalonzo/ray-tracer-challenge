namespace RayTracerChallenge.Test.Features;

public class Materials
{
    private const float Tolerance = 1E-4F;

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
        var eyev = Primitives.Vector(0, 0, -1F);
        var normalv = Primitives.Vector(0, 0, -1F);
        var light = new PointLight(Primitives.Point(0, 0, -10), Color.Create(1F, 1F, 1F));

        var result = m.Lighting(light, position, eyev, normalv);

        // Expect ambient, diffuse, and specular to all be at full strength.
        result.Should().Be(Color.Create(1.9F, 1.9F, 1.9F));
    }

    [Fact]
    public void Lighting_with_the_eye_between_light_and_surface_eye_offset_45()
    {
        var m = Material.Default;
        var position = Primitives.Point(0, 0, 0);
        var eyev = Primitives.Vector(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
        var normalv = Primitives.Vector(0, 0, -1F);
        var light = new PointLight(Primitives.Point(0, 0, -10), Color.Create(1F, 1F, 1F));

        var (ambient, diffuse, specular) = m.Lighting3(light, position, eyev, normalv);

        // Here, the ambient and diffuse components should be unchanged (because the angle between the light and normal vectors will not have changed),
        // but the specular value should have fallen off to (effectively) 0.
        ambient.X.Should().BeApproximately(0.1F, Tolerance);
        diffuse.X.Should().BeApproximately(0.9F, Tolerance);
        specular.X.Should().BeApproximately(0F, Tolerance);
    }

    [Fact]
    public void Lighting_with_eye_opposite_surface_light_offset_45()
    {
        var m = Material.Default;
        var position = Primitives.Point(0, 0, 0);
        var eyev = Primitives.Vector(0, 0, -1F);
        var normalv = Primitives.Vector(0, 0, -1F);
        var light = new PointLight(Primitives.Point(0, 10, -10), Color.Create(1F, 1F, 1F));

        var (ambient, diffuse, specular) = m.Lighting3(light, position, eyev, normalv);

        ambient.X.Should().BeApproximately(0.1F, Tolerance);
        diffuse.X.Should().BeApproximately(0.9F * MathF.Sqrt(2) / 2F, Tolerance);
        specular.X.Should().BeApproximately(0F, Tolerance);
    }

    [Fact]
    public void Lighting_with_eye_in_the_path_of_the_reflection_vector()
    {
        var m = Material.Default;
        var position = Primitives.Point(0, 0, 0);
        var eyev = Primitives.Vector(0, -MathF.Sqrt(2) / 2F, -MathF.Sqrt(2) / 2F);
        var normalv = Primitives.Vector(0, 0, -1F);
        var light = new PointLight(Primitives.Point(0, 10, -10), Color.Create(1F, 1F, 1F));

        var (ambient, diffuse, specular) = m.Lighting3(light, position, eyev, normalv);
        var result = m.Lighting(light, position, eyev, normalv);

        ambient.X.Should().BeApproximately(0.1F, Tolerance);
        diffuse.X.Should().BeApproximately(0.9F * MathF.Sqrt(2) / 2F, Tolerance);
        specular.X.Should().BeApproximately(0.9F, Tolerance);
        result.X.Should().BeApproximately(1.6364F, Tolerance);
    }

    [Fact]
    public void Lighting_with_the_light_behind_the_surface()
    {
        var m = Material.Default;
        var position = Primitives.Point(0, 0, 0);
        var eyev = Primitives.Vector(0, 0, -1F);
        var normalv = Primitives.Vector(0, 0, -1F);
        var light = new PointLight(Primitives.Point(0, 0, 10), Color.Create(1F, 1F, 1F));

        var (ambient, diffuse, specular) = m.Lighting3(light, position, eyev, normalv);

        ambient.X.Should().BeApproximately(0.1F, Tolerance);
        diffuse.X.Should().BeApproximately(0F, Tolerance);
        specular.X.Should().BeApproximately(0F, Tolerance);
    }
}
