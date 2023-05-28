using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Worlds
{
    [Fact]
    public void Creating_a_world()
    {
        var w = new World();

        w.Objects.Should().BeEmpty();
        w.LightSource.Should().BeNull();
    }

    [Fact]
    public void The_default_world()
    {
        var w = CreateDefaultWorld();

        w.Objects.Should().NotBeNullOrEmpty();
        w.LightSource.Should().NotBeNull();
    }

    [Fact]
    public void Intersect_a_world_with_a_ray()
    {
        var w = CreateDefaultWorld();
        var r = new Ray(Primitives.Point(0, 0, -5), Primitives.Vector(0, 0, 1));

        var xs = w.Intersect(r);

        xs.Should().HaveCount(4);
        xs[0].T.Should().Be(4);
        xs[1].T.Should().Be(4.5F);
        xs[2].T.Should().Be(5.5F);
        xs[3].T.Should().Be(6);
    }

    private static World CreateDefaultWorld()
    {
        var w = new World
        {
            LightSource = new PointLight(
                Primitives.Point(-10, 10, -10),
                Color.Create(1F, 1F, 1F)),
        };

        var s1 = new Sphere();
        var s2 = new Sphere();

        return w with
        {
            Objects = w.Objects
                .Add(s1 with
                    {
                        Material = s1.Material with
                        {
                            Color = Color.Create(0.8F, 1.0F, 0.6F),
                            Diffuse = 0.7F,
                            Specular = 0.2F
                        }
                    })
                .Add(s2 with { Transform = Matrix4x4.CreateScale(0.5F, 0.5F, 0.5F) })
        };
    }
}
