using System.Drawing;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IFaceDrawStrategy
    {
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, Light light, int width, int height,
            Point start);
    }
}