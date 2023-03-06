using Microsoft.Maui.Graphics.Skia;
using Microsoft.Maui.Graphics;
using SkiaSharp;

namespace RayTracerChallenge.Examples.ProjectileMotion;

public class ProjectileEnvironmentImageRenderer : IDisposable
{
    public ProjectileEnvironmentImageRenderer(int width, int height, int padding = 6)
    {
        _width = width;
        _height = height;
        _padding = padding;

        _bitmap = new SKBitmap(_width, _height);
        _canvas = new SKCanvas(_bitmap);
    }

    private readonly int _width;
    private readonly int _height;
    private readonly int _padding;

    private readonly SKBitmap _bitmap;
    private readonly SKCanvas _canvas;

    public IImage Render()
    {
        var p0 = new Projectile(
           new Vector3(1, 1, 0),
           Vector3.Normalize(new Vector3(1, 1, 0)));

        var env = new ProjectileEnvironment(
            // gravity -0.1 unit/tick
            new Vector3(0, -0.1f, 0),

            // wind is -0.01 unit/tick
            new Vector3(-0.01f, 0, 0));

        var trajectory = env.Trajectory(p0).ToList();

        var scaleX = (_width - _padding * 2) / trajectory.Max(t => t.Position.X);
        var scaleY = (_height - _padding * 2) / trajectory.Max(t => t.Position.Y);

        var paint = new SKPaint() { Color = new SKColor(255, 0, 0), IsAntialias = false };
        foreach (var p in trajectory)
        {
            _canvas.DrawCircle(
                _padding + p.Position.X * scaleX,
                _height - _padding - p.Position.Y * scaleY,
                2f,
                paint);
        }

        return new SkiaImage(_bitmap);
    }

    public void Dispose()
    {
        _canvas.Dispose();
        _bitmap.Dispose();
    }
}
