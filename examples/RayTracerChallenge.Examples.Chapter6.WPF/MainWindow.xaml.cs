using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using RayTracerChallenge.Core;
using SkiaSharp.Views.Desktop;

namespace RayTracerChallenge.Examples.Chapter6.WPF
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

        private const int imageWidth = 500;
        private const int imageHeight = 500;

        private SkiaRenderer? _renderer;

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            
            var info = e.Info;
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(0, 0, info.Width, info.Height);

            if (_renderer != null)
            {
                canvas.DrawImage(_renderer.Image, (info.Width - imageWidth) / 2, (info.Height - imageHeight) / 2, imageWidth, imageHeight);
            }
        }

        private async void OnRenderButtonClick(object sender, RoutedEventArgs e)
        {
            if (_renderer != null)
            {
                _renderer.Dispose();
            }

            _renderer = new SpherePhongRenderer(imageWidth, imageHeight);

            buttonRender.IsEnabled = false;

            var cts = new CancellationTokenSource();
            var refreshTask = DrawRefresh(25, cts.Token);            
            await _renderer.RenderAsync(CancellationToken.None);
            cts.Cancel();
            await refreshTask;

            buttonRender.IsEnabled = true;
        }

        private async Task DrawRefresh(int fps, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                paintSurface.InvalidateVisual();

                try
                {
                    await Task.Delay(1_000 / fps, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    continue;
                }
            }
        }
    }
}
