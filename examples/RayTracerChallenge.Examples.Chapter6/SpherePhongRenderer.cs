using RayTracerChallenge.Core;
using SkiaSharp;
using System.Numerics;

namespace RayTracerChallenge.Examples.Chapter6;

public class SpherePhongRenderer : SkiaRenderer
{
    public SpherePhongRenderer(int width, int height)
        : base(width, height)
    {
    }

    protected override void RenderCore(SKCanvas canvas)
    {
        var world = new World();
        world = world with
        {
            Objects = world.Objects.Add(new Sphere
            {
                Material = Material.Default with
                {
                    Color = Color.Create(1F, 0.2F, 1F)
                }
            }),
            LightSource = new PointLight(
                Primitives.Point(-10, 10, -10),
                Color.Create(1F, 1F, 1F))
        };

        canvas.DrawColor(new SKColor(0, 0, 0));

        var wallZ = 10f;
        var wallSize = 7f;

        var transformation = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateScale(wallSize / Width, -wallSize / Height, 1))
            .Append(Matrix4x4.CreateTranslation(-(wallSize / 2f), wallSize / 2f, 0))
            .Build();

        var rayOrigin = Primitives.Point(0, 0, -5);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var wallPosition = Vector4.Transform(Primitives.Point(x, y, wallZ), transformation);

                var ray = new Ray(
                    rayOrigin,
                    Vector4.Normalize(wallPosition - rayOrigin));

                var hit = world.Intersect(ray).Hit();
                if (hit != null)
                {
                    // Find the normal vector at the hit.
                    var point = ray.Position(hit.Value.T);
                    var normal = hit.Value.Object.NormalAt(point);
                    var eye = -ray.Direction;

                    var color = hit.Value.Object.Material.Lighting(world.LightSource, point, eye, normal);

                    var paint = new SKPaint()
                    {
                        Color = color.ToSKColor(),
                        IsAntialias = true
                    };

                    canvas.DrawPoint(x, y, paint);
                }
            }
        }
    }
}
