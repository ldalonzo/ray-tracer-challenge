using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace RayTracerChallenge.Test;

public class CanvasSkiaSharpUnitTest
{
    [Theory]
    [InlineData(320, 200)]
    public void CreatingCanvas(int width, int height)
    {
        using var bitmap = new SKBitmap(width, height);
        using var image = new SkiaImage(bitmap);

        image.Width.Should().Be(width);
        image.Height.Should().Be(height);

        // Every pixel is black
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var actual = bitmap.GetPixel(x, y);
                actual.Red.Should().Be(0);
                actual.Green.Should().Be(0);
                actual.Blue.Should().Be(0);
            }
        }
    }

    [Theory]
    [InlineData(320, 200, 2, 3)]
    public void WritingPixelToCanvas(int width, int height, int pointX, int pointY)
    {
        using var bitmap = new SKBitmap(width, height);
        using var image = new SkiaImage(bitmap);
        using var canvas = new SKCanvas(bitmap);

        var paint = new SKPaint() { Color = new SKColor(255, 0, 0), IsAntialias = false };
        canvas.DrawPoint(pointX, pointY, paint);

        var actual = bitmap.GetPixel(pointX, pointY);
        actual.Red.Should().Be(255);
        actual.Green.Should().Be(0);
        actual.Blue.Should().Be(0);
    }
}
