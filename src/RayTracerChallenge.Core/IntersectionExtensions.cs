namespace RayTracerChallenge.Core;

public static class IntersectionExtensions
{
    public static Intersection? Hit(this IEnumerable<Intersection> source)
    {
        // The hit will always be the intersection with the lowest nonnegative t value.
        var candidates = source
            .Where(s => s.T > 0)
            .OrderBy(s => s.T);

        if (candidates.Any())
        {
            return candidates.First();
        }

        return null;
    }
}
