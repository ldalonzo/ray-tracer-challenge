using System.Numerics;
using Vector = RayTracerChallenge.Core.Vector;

namespace RayTracerChallenge.Test.Features;

public class Tuples
{
    [Theory]
    [AutoData]
    public void A_tuple_with_w_eq_1_is_a_point(float x, float y, float z)
    {
        var a = new Vector4(x, y, z, 1.0f);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.W.Should().Be(1.0f);
        a.IsPoint().Should().BeTrue();
        a.IsVector().Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public void TupleIsAVector(float x, float y, float z)
    {
        var a = new Vector4(x, y, z, 0f);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.W.Should().Be(0f);
        a.IsPoint().Should().BeFalse();
        a.IsVector().Should().BeTrue();
    }

    /// <summary>
    /// point() creates tuples with w=1
    /// </summary>
    [Theory]
    [AutoData]
    public void PointFactory(float x, float y, float z)
    {
        var a = Point.Create(x, y, z);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.IsPoint().Should().BeTrue();
        a.IsVector().Should().BeFalse();
    }

    /// <summary>
    /// vector() creates tuples with w=0
    /// </summary>
    [Theory]
    [AutoData]
    public void VectorFactory(float x, float y, float z)
    {
        var a = Vector.Create(x, y, z);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.IsVector().Should().BeTrue();
        a.IsPoint().Should().BeFalse();
    }

    [Fact]
    public void AddingTwoTuples()
    {
        var a1 = new Vector4(3, -2, 5, 1);
        var a2 = new Vector4(-2, 3, 1, 0);

        var s = a1 + a2;

        s.X.Should().Be(1);
        s.Y.Should().Be(1);
        s.Z.Should().Be(6);
        s.W.Should().Be(1);
    }

    [Fact]
    public void SubtractingTwoPoints()
    {
        var p1 = Point.Create(3, 2, 1);
        var p2 = Point.Create(5, 6, 7);

        var v = p1 - p2;

        v.X.Should().Be(-2);
        v.Y.Should().Be(-4);
        v.Z.Should().Be(-6);
        v.W.Should().Be(0);
        v.IsPoint().Should().BeFalse();
        v.IsVector().Should().BeTrue();
    }

    [Fact]
    public void SubtractingVectorFromPoint()
    {
        var p = Point.Create(3, 2, 1);
        var v = Vector.Create(5, 6, 7);

        var q = p - v;

        q.X.Should().Be(-2);
        q.Y.Should().Be(-4);
        q.Z.Should().Be(-6);
        q.W.Should().Be(1);
        q.IsPoint().Should().BeTrue();
        q.IsVector().Should().BeFalse();
    }

    [Fact]
    public void SubtractingTwoVectors()
    {
        var v1 = Vector.Create(3, 2, 1);
        var v2 = Vector.Create(5, 6, 7);

        var v3 = v1 - v2;

        v3.X.Should().Be(-2);
        v3.Y.Should().Be(-4);
        v3.Z.Should().Be(-6);
        v3.W.Should().Be(0);
        v3.IsVector().Should().BeTrue();
        v3.IsPoint().Should().BeFalse();
    }

    [Fact]
    public void SubtractingVectorFromZeroVector()
    {
        var zero = Vector4.Zero;
        var v = Vector.Create(1, -2, 3);

        var nv = zero - v;
        nv.X.Should().Be(-1);
        nv.Y.Should().Be(2);
        nv.Z.Should().Be(-3);
        nv.W.Should().Be(0);
        nv.IsVector().Should().BeTrue();
        nv.IsPoint().Should().BeFalse();
    }

    [Fact]
    public void NegatingTuple()
    {
        var a = new Vector4(1, -2, 3, -4);
        var notA = -a;

        notA.X.Should().Be(-1);
        notA.Y.Should().Be(2);
        notA.Z.Should().Be(-3);
        notA.W.Should().Be(4);
    }

    [Fact]
    public void MultiplyingTupleByScalar()
    {
        var a = new Vector4(1, -2, 3, -4);
        var scalar = 3.5f;

        var b = a * scalar;

        b.X.Should().Be(3.5f);
        b.Y.Should().Be(-7f);
        b.Z.Should().Be(10.5f);
        b.W.Should().Be(-14f);
    }

    [Fact]
    public void DividingTupleByScalar()
    {
        var a = new Vector4(1, -2, 3, -4);
        var scalar = 2;

        var b = a / scalar;

        b.X.Should().Be(0.5f);
        b.Y.Should().Be(-1f);
        b.Z.Should().Be(1.5f);
        b.W.Should().Be(-2f);
    }

    [Theory]
    [InlineData(1, 0, 0, 1)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(0, 0, 1, 1)]
    [InlineData(2, 10, 11, 15)]
    [InlineData(-2, -10, -11, 15)]
    [InlineData(1, 2, 3, 3.741)]
    public void ComputingMagnitudeOfVector(float x, float y, float z, float expectedMagnitude)
    {
        var v = Vector.Create(x, y, z);

        v.Length().Should().BeApproximately(expectedMagnitude, 1E-3F);
    }

    public static readonly TheoryData<Vector4, Vector4> NormalizingVectorTheoryData = new()
    {
        { Vector.Create(4, 0, 0), Vector.Create(1, 0, 0) },
        { Vector.Create(1, 2, 3), Vector.Create(0.26726f, 0.53452f, 0.80178f) },
    };

    [Theory]
    [MemberData(nameof(NormalizingVectorTheoryData))]
    public void NormalizingVector(Vector4 v, Vector4 expected)
    {
        var actual = Vector4.Normalize(v);

        actual.X.Should().BeApproximately(expected.X, 1E-5f);
        actual.Y.Should().BeApproximately(expected.Y, 1E-5f);
        actual.Z.Should().BeApproximately(expected.Z, 1E-5f);
    }

    [Fact]
    public void DotProductOfTwoTuples()
    {
        var a = Vector.Create(1, 2, 3);
        var b = Vector.Create(2, 3, 4);

        Vector4.Dot(a, b).Should().Be(20);
    }

    [Fact]
    public void CrossProductOfTwoVectors()
    {
        var a = Vector.Create(1, 2, 3);
        var b = Vector.Create(2, 3, 4);

        Vector3 a3 = new(a.X, a.Y, a.Z);
        Vector3 b3 = new(b.X, b.Y, b.Z);

        Vector3.Cross(a3, b3).Should().Be(new Vector3(-1, 2, -1));
        Vector3.Cross(b3, a3).Should().Be(new Vector3(1, -2, 1));
    }

    [Fact]
    public void ColorsAreVector3()
    {
        var c = Color.Create(-0.5f, 0.4f, 1.7f);

        c.X.Should().Be(-0.5f);
        c.Y.Should().Be(0.4f);
        c.Z.Should().Be(1.7f);
    }

    [Fact]
    public void Adding_colors()
    {
        var c1 = Color.Create(0.9f, 0.6f, 0.75f);
        var c2 = Color.Create(0.7f, 0.1f, 0.25f);

        var c3 = c1 + c2;

        c3.X.Should().BeApproximately(1.6f, 1E-6F);
        c3.Y.Should().BeApproximately(0.7f, 1E-6F);
        c3.Z.Should().BeApproximately(1.0f, 1E-6F);
    }

    [Fact]
    public void Subtracting_colors()
    {
        var c1 = Color.Create(0.9f, 0.6f, 0.75f);
        var c2 = Color.Create(0.7f, 0.1f, 0.25f);

        var c3 = c1 - c2;

        c3.X.Should().BeApproximately(0.2f, 1E-6F);
        c3.Y.Should().BeApproximately(0.5f, 1E-6F);
        c3.Z.Should().BeApproximately(0.5f, 1E-6F);
    }

    [Fact]
    public void Multiplying_a_color_by_a_scalar()
    {
        var c = Color.Create(0.2f, 0.3f, 0.4f);
        var s = 2;

        var m = c * s;

        m.X.Should().BeApproximately(0.4f, 1E-6F);
        m.Y.Should().BeApproximately(0.6f, 1E-6F);
        m.Z.Should().BeApproximately(0.8f, 1E-6F);
    }

    [Fact]
    public void Multiplying_colors()
    {
        var c1 = Color.Create(1f, 0.2f, 0.4f);
        var c2 = Color.Create(0.9f, 1f, 0.1f);

        var c3 = c1 * c2;

        c3.X.Should().BeApproximately(0.9f, 1E-6F);
        c3.Y.Should().BeApproximately(0.2f, 1E-6F);
        c3.Z.Should().BeApproximately(0.04f, 1E-6F);
    }
}
