using System.Drawing;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IPolygonDrawer
    {
        void Draw(PixelBuffer pixelBuffer, Polygon polygon, Light light, ZBuffer zBuffer, int width, int height, Point startPoint);
    }
}