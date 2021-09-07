using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class PolygonDrawer : IPolygonDrawer
    {
        private readonly IFaceDrawStrategy _faceDrawStrategy;

        public PolygonDrawer(IFaceDrawStrategy faceDrawStrategy)
        {
            _faceDrawStrategy = faceDrawStrategy;
        }
        
        public void Draw(PixelBuffer pixelBuffer, Polygon polygon, int width, int height, Point startPoint)
        {
            foreach (Face face in polygon.Faces)
            {
                _faceDrawStrategy.DrawFace(pixelBuffer, polygon, face, width, height, startPoint);
            }
        }
    }
}