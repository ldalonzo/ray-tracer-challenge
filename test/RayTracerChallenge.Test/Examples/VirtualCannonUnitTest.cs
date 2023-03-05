using System.Numerics;
using RayTracerChallenge.Examples.ProjectileMotion;

namespace RayTracerChallenge.Test.Examples;

public class VirtualCannonUnitTest
{
    [Fact]
    public void Trajectory()
    {
        var p = new Projectile(

            // projectile starts one unit above the origin.
            new Vector3(1, 1, 0),

            // velocity is normalized to 1 unit / tick.
            Vector3.Normalize(new Vector3(1, 1, 0)));

        var e = new ProjectileEnvironment(
            // gravity -0.1 unit/tick
            new Vector3(0, -0.1f, 0),

            // wind is -0.01 unit/tick
            new Vector3(-0.01f, 0, 0));

        var traj = e.Trajectory(p).ToList();

        traj.Count().Should().BePositive();
        traj.Last().Position.Y.Should().BeApproximately(0.313f, 1E-3f);
        traj.Last().Position.X.Should().BeApproximately(11.114f, 1E-3f);
    }
}
