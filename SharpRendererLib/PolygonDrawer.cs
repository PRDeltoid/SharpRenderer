using System.Drawing;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class PolygonDrawer : IPolygonDrawer
    {
        private readonly IFaceDrawStrategy _faceDrawStrategy;

        public PolygonDrawer(IFaceDrawStrategy faceDrawStrategy)
        {
            _faceDrawStrategy = faceDrawStrategy;
        }
        
        public void Draw(PixelBuffer pixelBuffer, Polygon polygon, Light light, ZBuffer zBuffer)
        {
            foreach (Face face in polygon.Faces)
            {
                _faceDrawStrategy.DrawFace(pixelBuffer, face, light, zBuffer);
            }
        }
    }
}