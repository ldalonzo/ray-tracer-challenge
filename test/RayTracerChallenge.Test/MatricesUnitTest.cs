using System.Numerics;

namespace RayTracerChallenge.Test;

public class MatricesUnitTest
{
    [Fact]
    public void Constructing_and_inspecting_a_4x4_matrix()
    {
        var m = new Matrix4x4(
              1, 2, 3, 4,
            5.5f, 6.5f, 7.5f, 8.5f,
            9, 10, 11, 12,
            13.5f, 14.5f, 15.5f, 16.5f);

        m.M11.Should().Be(1);
        m.M14.Should().Be(4);
        m.M21.Should().Be(5.5f);
        m.M23.Should().Be(7.5f);
        m.M33.Should().Be(11);
        m.M41.Should().Be(13.5f);
        m.M43.Should().Be(15.5f);
    }

    [Fact]
    public void Matrix_equality_with_identical_matrices()
    {
        var a = new Matrix4x4(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);

        var b = new Matrix4x4(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);

        a.Should().NotBeSameAs(b);
        a.Should().Be(b);
    }

    [Fact]
    public void Matrix_equality_with_different_matrices()
    {
        var a = new Matrix4x4(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);

        var b = new Matrix4x4(
            2, 3, 4, 5,
            6, 7, 8, 9,
            8, 7, 6, 5,
            4, 3, 2, 1);

        a.Should().NotBeSameAs(b);
        a.Should().NotBe(b);
    }

    [Fact]
    public void Multiplying_two_matrices()
    {
        var a = new Matrix4x4(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);

        var b = new Matrix4x4(
            -2, 1, 2, 3,
            3, 2, 1, -1,
            4, 3, 6, 5,
            1, 2, 7, 8);

        var c = a * b;

        c.M11.Should().Be(20);
        c.M12.Should().Be(22);
        c.M13.Should().Be(50);
        c.M14.Should().Be(48);
        c.M21.Should().Be(44);
        c.M22.Should().Be(54);
        c.M23.Should().Be(114);
        c.M24.Should().Be(108);
        c.M31.Should().Be(40);
        c.M32.Should().Be(58);
        c.M33.Should().Be(110);
        c.M34.Should().Be(102);
        c.M41.Should().Be(16);
        c.M42.Should().Be(26);
        c.M43.Should().Be(46);
        c.M44.Should().Be(42);
    }

    [Fact]
    public void A_matrix_multiplied_by_a_tuple()
    {
        var a = new Matrix4x4(
           1, 2, 3, 4,
           2, 4, 4, 2,
           8, 6, 4, 1,
           0, 0, 0, 1);

        var b = new Vector4(1, 2, 3, 1);

        var c = Vector4.Transform(b, Matrix4x4.Transpose(a));

        c.X.Should().Be(18);
        c.Y.Should().Be(24);
        c.Z.Should().Be(33);
        c.W.Should().Be(1);
    }

    [Fact]
    public void Multiplying_a_matrix_by_the_identity_matrix()
    {
        var a = new Matrix4x4(
            0, 1, 2, 4,
            1, 2, 4, 8,
            2, 4, 8, 16,
            4, 8, 16, 32);

        var b = a * Matrix4x4.Identity;

        b.Should().NotBeSameAs(a);
        b.Should().Be(a);
    }

    [Fact]
    public void Multiplying_the_identity_matrix_by_a_tuple()
    {
        var a = new Vector4(1, 2, 3, 4);

        var b = Vector4.Transform(a, Matrix4x4.Identity);

        b.Should().NotBeSameAs(a);
        b.Should().Be(a);
    }

    [Fact]
    public void Transposing_the_identity_matrix()
    {
        var a = Matrix4x4.Identity;

        var b = Matrix4x4.Transpose(a);

        b.Should().NotBeSameAs(a);
        b.Should().Be(a);
    }

    [Fact]
    public void Testing_an_invertible_matrix_for_invertibility()
    {
        var a = new Matrix4x4(
            6, 4, 4, 4,
            5, 5, 7, 6,
            4, -9, 3, -7,
            9, 1, 7, -6);

        var determinant = a.GetDeterminant();

        determinant.Should().BeApproximately(-2_120, 1E-6f);
    }

    [Fact]
    public void Testing_a_noninvertible_matrix_for_invertibility()
    {
        var a = new Matrix4x4(
            -4, 2, -2, -3,
            9, 6, 2, 6,
            0, -5, 1, -5,
            0, 0, 0, 0);

        var determinant = a.GetDeterminant();

        determinant.Should().BeApproximately(0, 1E-6f);
    }

    [Fact]
    public void Calculating_the_inverse_of_a_matrix()
    {
        var a = new Matrix4x4(
            -5, 2, 6, -8,
            1, -5, 1, 8,
            7, 7, -6, -7,
            1, -3, 7, 4);

        Matrix4x4.Invert(a, out var b).Should().BeTrue();

        b.M11.Should().BeApproximately(0.21805f, 1E-5f);
        b.M12.Should().BeApproximately(0.45113f, 1E-5f);

        b.M21.Should().BeApproximately(-0.80827f, 1E-5f);
        b.M22.Should().BeApproximately(-1.45677f, 1E-5f);

        b.M33.Should().BeApproximately(-0.05263f, 1E-5f);
        b.M34.Should().BeApproximately(0.19737f, 1E-5f);

        b.M43.Should().BeApproximately(-0.30075f, 1E-5f);
        b.M44.Should().BeApproximately(0.30639f, 1E-5f);
    }

    [Fact]
    public void Multiplying_a_product_by_its_inverse()
    {
        var a = new Matrix4x4(
            3, -9, 7, 3,
            3, -8, 2, -9,
            -4, 4, 4, 1,
            -6, 5, -1, 1);

        var b = new Matrix4x4(
            8, 2, 2, 2,
            3, -1, 7, 0,
            7, 0, 5, 4,
            6, -2, 0, 5);

        var c = a * b;
        Matrix4x4.Invert(b, out var b_i).Should().BeTrue();
        var d = c * b_i;

        d.M11.Should().BeApproximately(a.M11, 1e-5f);
        d.M12.Should().BeApproximately(a.M12, 1e-5f);
        d.M13.Should().BeApproximately(a.M13, 1e-5f);
        d.M14.Should().BeApproximately(a.M14, 1e-5f);

        d.M21.Should().BeApproximately(a.M21, 1e-5f);
        d.M22.Should().BeApproximately(a.M22, 1e-5f);
        d.M23.Should().BeApproximately(a.M23, 1e-5f);
        d.M24.Should().BeApproximately(a.M24, 1e-5f);

        d.M31.Should().BeApproximately(a.M31, 1e-5f);
        d.M32.Should().BeApproximately(a.M32, 1e-5f);
        d.M33.Should().BeApproximately(a.M33, 1e-5f);
        d.M34.Should().BeApproximately(a.M34, 1e-5f);

        d.M41.Should().BeApproximately(a.M41, 1e-5f);
        d.M42.Should().BeApproximately(a.M42, 1e-5f);
        d.M43.Should().BeApproximately(a.M43, 1e-5f);
        d.M44.Should().BeApproximately(a.M44, 1e-5f);
    }
}
