namespace RayTracerChallenge.Examples.ProjectileMotion;

public record ProjectileEnvironment(Vector3 Gravity, Vector3 Wind)
{
    public Projectile Tick(Projectile projectile) => new(
            projectile.Position + projectile.Velocity,
            projectile.Velocity + Gravity + Wind);

    public IEnumerable<Projectile> Trajectory(Projectile p0)
    {
        var p = p0;
        while (p.Position.Y > 0)
        {
            yield return p;
            p = Tick(p);
        }
    }
}

