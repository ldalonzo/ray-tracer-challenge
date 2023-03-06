using System.Numerics;
using FluentAssertions;
using SkiaSharp;

namespace RayTracerChallenge.Test;

public class MatricesUnitTest
{
    [Fact]
    public void ConstructingAndInspectingA4x4Matrix()
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
    public void MatrixEqualityWithIdenticalMatrices()
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
    public void MatrixEqualityWithDifferentMatrices()
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
    public void MultiplyingTwoMatrices()
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
    public void MatrixMultipliedByTuple()
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
}
