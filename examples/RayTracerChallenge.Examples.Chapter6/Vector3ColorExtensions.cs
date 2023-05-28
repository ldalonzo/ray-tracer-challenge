using SkiaSharp;
using System.Numerics;

namespace RayTracerChallenge.Examples.Chapter6;

public static class Vector3ColorExtensions
{
    public static SKColor ToSKColor(this Vector3 color)
    {
        var c = color * 255;
        return new SKColor(ToByte(c.X), ToByte(c.Y), ToByte(c.Z));
    }   

    private static byte ToByte(float value)
    {
        if (value > 255)
        {
            return 255;
        }

        if (value < 0)
        {
            return 0;
        }

        return (byte)value;
    }
}


