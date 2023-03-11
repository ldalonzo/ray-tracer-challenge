using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Transformations
{
    [Fact]
    public void Multiplying_by_a_translation_matrix()
    {
        var transform = Matrix4x4.CreateTranslation(new Vector3(5, -3, 2));
        var p = Point.Create(-3, 4, 5);

        var t = Vector4.Transform(p, transform);

        t.X.Should().Be(2);
        t.Y.Should().Be(1);
        t.Z.Should().Be(7);
        t.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_by_the_inverse_of_a_translation_matrix()
    {
        var transform = Matrix4x4.CreateTranslation(new Vector3(5, -3, 2));
        Matrix4x4.Invert(transform, out var inv).Should().BeTrue();
        var p = Point.Create(-3, 4, 5);

        var t = Vector4.Transform(p, inv);

        t.X.Should().Be(-8);
        t.Y.Should().Be(7);
        t.Z.Should().Be(3);
        t.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Translation_does_not_affect_vectors()
    {
        var transform = Matrix4x4.CreateTranslation(new Vector3(5, -3, 2));
        var v = Vector.Create(-3, 4, 5);

        var t = Vector4.Transform(v, transform);

        t.Should().NotBeSameAs(v);
        t.Should().Be(v);
    }
}
