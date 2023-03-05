using System.Numerics;
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
         => DrawTrajectory(e.Surface.Canvas, e.Info);

    private void DrawTrajectory(SKCanvas canvas, SKImageInfo info)
    {
        var padding = 6;

        var p0 = new Projectile(
           new Vector3(1, 1, 0),
           Vector3.Normalize(new Vector3(1, 1, 0)));

        var env = new ProjectileEnvironment(
            // gravity -0.1 unit/tick
            new Vector3(0, -0.1f, 0),

            // wind is -0.01 unit/tick
            new Vector3(-0.01f, 0, 0));

        var trajectory = env.Trajectory(p0).ToList();

        var scaleX = (info.Width - padding * 2) / trajectory.Max(z => z.Position.X);
        var scaleY = (info.Height - padding * 2) / trajectory.Max(z => z.Position.Y);

        var paint = new SKPaint() { Color = new SKColor(255, 0, 0), IsAntialias = false };
        foreach (var p in trajectory)
        {
            canvas.DrawCircle(
                padding + p.Position.X * scaleX,
                info.Height - padding - p.Position.Y * scaleY,
                2.5f,
                paint);
        }
    }
}
