using SkiaSharp;
using System.Numerics;
using RayTracerChallenge.Core;

namespace RayTracerChallenge.Examples.Chapter5;

public class SphereSilhouetteRenderer : SkiaRenderer
{
    public SphereSilhouetteRenderer(int width, int height)
        : base(width, height)
    {
    }

    protected override void RenderCore(SKCanvas canvas)
    {
        var rayOrigin = Primitives.Point(0, 0, -5);
        var wallZ = 10f;
        var wallSize = 7f;

        var shape = new Sphere();

        canvas.DrawColor(new SKColor(0, 0, 0));

        var paint = new SKPaint() { Color = new SKColor(255, 0, 0), IsAntialias = true };

        var transformation = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateScale(wallSize / Width, -wallSize / Height, 1))
            .Append(Matrix4x4.CreateTranslation(-(wallSize / 2f), wallSize / 2f, 0))
            .Build();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var wallPosition = Vector4.Transform(Primitives.Point(x, y, wallZ), transformation);

                var ray = new Ray(
                    rayOrigin,
                    Vector4.Normalize(wallPosition - rayOrigin));

                if (shape.Intersect(ray).Hit() != null)
                {
                    canvas.DrawPoint(x, y, paint);
                }
            }
        }
    }
}
