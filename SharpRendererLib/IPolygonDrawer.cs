using System.Drawing;

namespace SharpRendererLib
{
    public interface IPolygonDrawer
    {
        void Draw(PixelBuffer pixelBuffer, Polygon polygon, int width, int height, Point startPoint);
    }
}