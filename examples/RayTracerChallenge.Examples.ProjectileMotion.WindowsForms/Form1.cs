using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace RayTracerChallenge.Examples.ProjectileMotion.WindowsForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void skControl1_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        e.Surface.Canvas.Clear(SKColor.Parse("#003366"));

        var canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
        Draw(canvas, e.Info);
    }

    private void skglControl1_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
    {
        e.Surface.Canvas.Clear(SKColor.Parse("#000000"));

        var canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
        Draw(canvas, e.Info);
    }

    private void Draw(ICanvas canvas, SKImageInfo info)
    {
        using var renderer = new ProjectileEnvironmentImageRenderer(info.Width, info.Height);
        using var image = renderer.Render();

        canvas.DrawImage(image, 0, 0, info.Width, info.Height);
    }
}
