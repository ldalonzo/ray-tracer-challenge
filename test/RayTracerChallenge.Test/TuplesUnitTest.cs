using System.Numerics;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace RayTracerChallenge.Test;

public class TuplesUnitTest
{
    /// <summary>
    /// A tuple with w=1.0 is a point
    /// </summary>
    [Theory]
    [InlineAutoData(1.0)]
    public void TupleIsAPoint(float w, float x, float y, float z)
    {
        var a = new Vector4(x, y, z, w);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.W.Should().Be(w);
        a.IsPoint().Should().BeTrue();
        a.IsVector().Should().BeFalse();
    }

    /// <summary>
    /// A tuple with w=0 is a vector
    /// </summary>
    [Theory]
    [InlineAutoData(0.0)]
    public void TupleIsAVector(float w, float x, float y, float z)
    {
        var a = new Vector4(x, y, z, w);

        a.X.Should().Be(x);
        a.Y.Should().Be(y);
        a.Z.Should().Be(z);
        a.W.Should().Be(w);
        a.IsPoint().Should().BeFalse();
        a.IsVector().Should().BeTrue();
    }
}
