using Microsoft.Maui.Graphics.Skia;
using Microsoft.Maui.Graphics;
using SkiaSharp;

namespace RayTracerChallenge.Core;

public abstract class SkiaRenderer : IDisposable
{
    public SkiaRenderer(int width, int height)
    {
        _bitmap = new SKBitmap(width, height);
        _canvas = new SKCanvas(_bitmap);
    }

    protected int Width => _bitmap.Width;
    protected int Height => _bitmap.Height;

    private readonly SKBitmap _bitmap;
    private readonly SKCanvas _canvas;

    public IImage Image => new SkiaImage(_bitmap);

    public Task RenderAsync(CancellationToken cancel)
    {
        return Task.Run(() => RenderCore(_canvas), cancel);
    }

    protected abstract void RenderCore(SKCanvas canvas);

    public void Dispose()
    {
        _canvas.Dispose();
        _bitmap.Dispose();
    }
}
