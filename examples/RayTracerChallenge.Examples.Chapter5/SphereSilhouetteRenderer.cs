using Microsoft.Maui.Graphics.Skia;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using System.Numerics;
using RayTracerChallenge.Core;

namespace RayTracerChallenge.Examples.Chapter5;

public class SphereSilhouetteRenderer : IDisposable
{
    public SphereSilhouetteRenderer(int width, int height)
    {
        _width = width;
        _height = height;

        _bitmap = new SKBitmap(_width, _height);
        _canvas = new SKCanvas(_bitmap);
    }

    private readonly int _width;
    private readonly int _height;

    private readonly SKBitmap _bitmap;
    private readonly SKCanvas _canvas;

    public IImage Image => new SkiaImage(_bitmap);

    public Task RenderAsync(CancellationToken cancel)
    {
        return Task.Run(() => RenderCore(), cancel);
    }

    private void RenderCore()
    {
        var rayOrigin = Primitives.Point(0, 0, -5);
        var wallZ = 10f;
        var wallSize = 7f;

        var shape = new Sphere();

        _canvas.DrawColor(new SKColor(0, 0, 0));

        var paint = new SKPaint() { Color = new SKColor(255, 0, 0), IsAntialias = true };

        var transformation = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateScale(wallSize / _width, -wallSize / _height, 1))
            .Append(Matrix4x4.CreateTranslation(-(wallSize / 2f), wallSize / 2f, 0))
            .Build();

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var wallPosition = Vector4.Transform(Primitives.Point(x, y, wallZ), transformation);

                var ray = new Ray(
                    rayOrigin,
                    Vector4.Normalize(wallPosition - rayOrigin));

                if (shape.Intersect(ray).Hit() != null)
                {
                    _canvas.DrawPoint(x, y, paint);
                }
            }
        }
    }

    public void Dispose()
    {
        _canvas.Dispose();
        _bitmap.Dispose();
    }
}
