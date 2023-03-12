using System.Windows;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp.Views.Desktop;

namespace RayTracerChallenge.Examples.Clock.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

            var renderer = new AnalogClockRenderer();
            renderer.Render(canvas, e.Info);
        }
    }
}
