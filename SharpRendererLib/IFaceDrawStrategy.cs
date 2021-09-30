using System.Drawing;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IFaceDrawStrategy
    {
        void DrawFace(PixelBuffer pixelBuffer, Face face, Light light, ZBuffer zBuffer, int width, int height,
            Point startPoint);
    }
}