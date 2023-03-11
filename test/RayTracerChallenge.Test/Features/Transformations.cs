using System.Numerics;
using Vector = RayTracerChallenge.Core.Vector;

namespace RayTracerChallenge.Test.Features;

public class Transformations
{
    [Fact]
    public void Multiplying_by_a_translation_matrix()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        var p = Point.Create(-3, 4, 5);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(2);
        pt.Y.Should().Be(1);
        pt.Z.Should().Be(7);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_by_the_inverse_of_a_translation_matrix()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        Matrix4x4.Invert(transform, out var inv).Should().BeTrue();
        var p = Point.Create(-3, 4, 5);

        var pt = Vector4.Transform(p, inv);

        pt.X.Should().Be(-8);
        pt.Y.Should().Be(7);
        pt.Z.Should().Be(3);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Translation_does_not_affect_vectors()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        var v = Vector.Create(-3, 4, 5);

        var vt = Vector4.Transform(v, transform);

        vt.Should().NotBeSameAs(v);
        vt.Should().Be(v);
    }

    [Fact]
    public void A_scaling_matrix_applied_to_a_point()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        var p = Point.Create(-4, 6, 8);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(-8);
        pt.Y.Should().Be(18);
        pt.Z.Should().Be(32);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_scaling_matrix_applied_to_a_vector()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        var v = Vector.Create(-4, 6, 8);

        var vt = Vector4.Transform(v, transform);

        vt.X.Should().Be(-8);
        vt.Y.Should().Be(18);
        vt.Z.Should().Be(32);
        vt.IsVector().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_by_the_inverse_of_a_scaling_matrix()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        Matrix4x4.Invert(transform, out var inv).Should().BeTrue();
        var v = Vector.Create(-4, 6, 8);

        var vt = Vector4.Transform(v, inv);

        vt.X.Should().Be(-2);
        vt.Y.Should().Be(2);
        vt.Z.Should().Be(2);
        vt.IsVector().Should().BeTrue();
    }

    [Fact]
    public void Reflection_is_scaling_by_a_negative_value()
    {
        // Shows how a point can be reflected across the x axis by scaling the x component by -1.
        var transform = Matrix4x4.CreateScale(-1, 1, 1);
        var p = Point.Create(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(-2);
        pt.Y.Should().Be(3);
        pt.Z.Should().Be(4);
        pt.IsPoint().Should().BeTrue();
    }
}
