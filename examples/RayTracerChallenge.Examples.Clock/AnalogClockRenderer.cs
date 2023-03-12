using System.Numerics;
using Microsoft.Maui.Graphics;
using RayTracerChallenge.Core;

namespace RayTracerChallenge.Examples.Clock;

public class AnalogClockRenderer
{
    private const int Padding = 12;

    public void Render(ICanvas canvas, SkiaSharp.SKImageInfo info)
    {
        canvas.FillColor = Colors.Navy;
        canvas.FillRectangle(0, 0, info.Width, info.Height);

        var transformation = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateScale((MathF.Min(info.Width, info.Height) - Padding) / 2f))
            .Append(Matrix4x4.CreateRotationZ(-MathF.PI / 2))
            .Append(Matrix4x4.CreateTranslation(info.Width / 2, info.Height / 2, 0))
            .Build();

        canvas.StrokeColor = Colors.Red;
        foreach (var p in EnumeratePoints().Select(p => Vector4.Transform(p, transformation)))
        {
            canvas.DrawCircle(p.X, p.Y, 2);
        }
    }

    private IEnumerable<Vector4> EnumeratePoints()
    {
        yield return Primitives.Point(0, 0, 0);

        var p = Primitives.Point(1, 0, 0);
        yield return p;

        var transformation = new Transformation4x4Builder()
            .Append(Matrix4x4.CreateRotationZ(MathF.PI / 6))
            .Build();

        for (int i = 0; i < 11; i++)
        {
            p = Vector4.Transform(p, transformation);
            yield return p;
        }
    }
}
