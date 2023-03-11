using System.Numerics;
using Vector = RayTracerChallenge.Core.Vector;

namespace RayTracerChallenge.Test.Features;

public class Transformations
{
    private const float Tolerance = 1E-6F;

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

    [Fact]
    public void Rotating_a_point_around_the_x_axis()
    {
        var halfQuarter = Matrix4x4.CreateRotationX(MathF.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationX(MathF.PI / 2);
        var p = Point.Create(0, 1, 0);

        var p1 = Vector4.Transform(p, halfQuarter);
        var p2 = Vector4.Transform(p, fullQuarter);

        p1.X.Should().BeApproximately(0, Tolerance);
        p1.Y.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.Z.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.IsPoint().Should().BeTrue();

        p2.X.Should().BeApproximately(0, Tolerance);
        p2.Y.Should().BeApproximately(0, Tolerance);
        p2.Z.Should().BeApproximately(1, Tolerance);
        p2.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void The_inverse_of_an_x_rotation_rotates_in_the_opposite_direction()
    {
        var halfQuarter = Matrix4x4.CreateRotationX(MathF.PI / 4);
        Matrix4x4.Invert(halfQuarter, out var inv).Should().BeTrue();
        var p = Point.Create(0, 1, 0);

        var p1 = Vector4.Transform(p, inv);

        p1.X.Should().BeApproximately(0, Tolerance);
        p1.Y.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.Z.Should().BeApproximately(-MathF.Sqrt(2) / 2, Tolerance);
        p1.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Rotating_a_point_around_the_y_axis()
    {
        var halfQuarter = Matrix4x4.CreateRotationY(MathF.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationY(MathF.PI / 2);
        var p = Point.Create(0, 0, 1);

        var p1 = Vector4.Transform(p, halfQuarter);
        var p2 = Vector4.Transform(p, fullQuarter);

        
        p1.X.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.Y.Should().BeApproximately(0, Tolerance);
        p1.Z.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.IsPoint().Should().BeTrue();

        p2.X.Should().BeApproximately(1, Tolerance);
        p2.Y.Should().BeApproximately(0, Tolerance);
        p2.Z.Should().BeApproximately(0, Tolerance);
        p2.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Rotating_a_point_around_the_Z_axis()
    {
        var halfQuarter = Matrix4x4.CreateRotationZ(MathF.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationZ(MathF.PI / 2);
        var p = Point.Create(0, 1, 0);

        var p1 = Vector4.Transform(p, halfQuarter);
        var p2 = Vector4.Transform(p, fullQuarter);

        p1.X.Should().BeApproximately(-MathF.Sqrt(2) / 2, Tolerance);
        p1.Y.Should().BeApproximately(MathF.Sqrt(2) / 2, Tolerance);
        p1.Z.Should().BeApproximately(0, Tolerance);
        p1.IsPoint().Should().BeTrue();

        p2.X.Should().BeApproximately(-1, Tolerance);
        p2.Y.Should().BeApproximately(0, Tolerance);
        p2.Z.Should().BeApproximately(0, Tolerance);
        p2.IsPoint().Should().BeTrue();
    }
}
