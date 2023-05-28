using System.Collections.Immutable;

namespace RayTracerChallenge.Core;

public record World
{
    public IImmutableList<ISceneObject> Objects { get; init; } = ImmutableList.Create<ISceneObject>();

    public PointLight? LightSource { get; init; }

    public IReadOnlyList<Intersection> Intersect(Ray worldRay) => Objects
        .SelectMany(o => o.Intersect(worldRay))
        .OrderBy(i => i.T)
        .ToList();
}
