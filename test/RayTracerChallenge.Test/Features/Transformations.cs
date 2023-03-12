using System.Numerics;

namespace RayTracerChallenge.Test.Features;

public class Transformations
{
    private const float Tolerance = 1E-6F;

    [Fact]
    public void Multiplying_by_a_translation_matrix()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        var p = Primitives.Point(-3, 4, 5);

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
        var p = Primitives.Point(-3, 4, 5);

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
        var v = Primitives.Vector(-3, 4, 5);

        var vt = Vector4.Transform(v, transform);

        vt.Should().NotBeSameAs(v);
        vt.Should().Be(v);
    }

    [Fact]
    public void A_scaling_matrix_applied_to_a_point()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        var p = Primitives.Point(-4, 6, 8);

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
        var v = Primitives.Vector(-4, 6, 8);

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
        var v = Primitives.Vector(-4, 6, 8);

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
        var p = Primitives.Point(2, 3, 4);

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
        var p = Primitives.Point(0, 1, 0);

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
        var p = Primitives.Point(0, 1, 0);

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
        var p = Primitives.Point(0, 0, 1);

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
        var p = Primitives.Point(0, 1, 0);

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

    [Fact]
    public void A_shearing_transformation_moves_x_in_proportion_to_y()
    {
        var transform = new Transformation4x4Builder().WithShearing(1, 0, 0, 0, 0, 0).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(5);
        pt.Y.Should().Be(3);
        pt.Z.Should().Be(4);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_shearing_transformation_moves_x_in_proportion_to_z()
    {
        var transform = new Transformation4x4Builder().WithShearing(0, 1, 0, 0, 0, 0).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(6);
        pt.Y.Should().Be(3);
        pt.Z.Should().Be(4);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_shearing_transformation_moves_y_in_proportion_to_x()
    {
        var transform = new Transformation4x4Builder().WithShearing(0, 0, 1, 0, 0, 0).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(2);
        pt.Y.Should().Be(5);
        pt.Z.Should().Be(4);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_shearing_transformation_moves_y_in_proportion_to_z()
    {
        var transform = new Transformation4x4Builder().WithShearing(0, 0, 0, 1, 0, 0).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(2);
        pt.Y.Should().Be(7);
        pt.Z.Should().Be(4);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_shearing_transformation_moves_z_in_proportion_to_x()
    {
        var transform = new Transformation4x4Builder().WithShearing(0, 0, 0, 0, 1, 0).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(2);
        pt.Y.Should().Be(3);
        pt.Z.Should().Be(6);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void A_shearing_transformation_moves_z_in_proportion_to_y()
    {
        var transform = new Transformation4x4Builder().WithShearing(0, 0, 0, 0, 0, 1).Build();
        var p = Primitives.Point(2, 3, 4);

        var pt = Vector4.Transform(p, transform);

        pt.X.Should().Be(2);
        pt.Y.Should().Be(3);
        pt.Z.Should().Be(7);
        pt.IsPoint().Should().BeTrue();
    }

    [Fact]
    public void Individual_transformations_are_applied_in_sequence()
    {
        var p = Primitives.Point(1, 0, 1);
        var a = Matrix4x4.CreateRotationX(MathF.PI / 2);
        var b = Matrix4x4.CreateScale(5, 5, 5);
        var c = Matrix4x4.CreateTranslation(10, 5, 7);

        // apply rotation first
        var p2 = Vector4.Transform(p, a);
        p2.X.Should().BeApproximately(1, Tolerance);
        p2.Y.Should().BeApproximately(-1, Tolerance);
        p2.Z.Should().BeApproximately(0, Tolerance);

        // then apply scaling
        var p3 = Vector4.Transform(p2, b);
        p3.X.Should().BeApproximately(5, Tolerance);
        p3.Y.Should().BeApproximately(-5, Tolerance);
        p3.Z.Should().BeApproximately(0, Tolerance);

        // then apply translation
        var p4 = Vector4.Transform(p3, c);
        p4.X.Should().BeApproximately(15, Tolerance);
        p4.Y.Should().BeApproximately(0, Tolerance);
        p4.Z.Should().BeApproximately(7, Tolerance);
    }

    [Fact]
    public void Chained_transformations_must_be_applied_in_reverse_order()
    {
        var p = Primitives.Point(1, 0, 1);
        var a = Matrix4x4.CreateRotationX(MathF.PI / 2);
        var b = Matrix4x4.CreateScale(5, 5, 5);
        var c = Matrix4x4.CreateTranslation(10, 5, 7);

        var t = Matrix4x4.Multiply(Matrix4x4.Multiply(a, b), c);

        var pt= Vector4.Transform(p, t);
        pt.X.Should().BeApproximately(15, Tolerance);
        pt.Y.Should().BeApproximately(0, Tolerance);
        pt.Z.Should().BeApproximately(7, Tolerance);
    }

    [Fact]
    public void Chained_transformations()
    {
        var p = Primitives.Point(1, 0, 1);

        var t = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateRotationX(MathF.PI / 2))
            .Append(Matrix4x4.CreateScale(5, 5, 5))
            .Append(Matrix4x4.CreateTranslation(10, 5, 7))
            .Build();

        var pt = Vector4.Transform(p, t);
        pt.X.Should().BeApproximately(15, Tolerance);
        pt.Y.Should().BeApproximately(0, Tolerance);
        pt.Z.Should().BeApproximately(7, Tolerance);
    }
}
