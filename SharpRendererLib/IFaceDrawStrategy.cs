using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public interface IFaceDrawStrategy
    {
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, int width, int height,
            Point start);
    }
}