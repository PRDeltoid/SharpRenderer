using System.Drawing;
using System.Numerics;
using SharpRendererLib.Extensions;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FlatFaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly IFlatColorDrawStrategy _colorDrawStrategy;

        public FlatFaceDrawStrategy(IFlatColorDrawStrategy colorDrawStrategy)
        {
            _colorDrawStrategy = colorDrawStrategy;
        }
        
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, Light light, ZBuffer zBuffer, int width, int height, Point start)
        {
            // Get a vector that is normal (perpendicular) to the face
            Vector3 faceNormal = FaceHelper.GetFaceNormal(face);
            
            // Determine the intensity of light hitting the face
            float intensity = Vector3.Dot(faceNormal, light); 
            // If the intensity is negative, the light is coming from behind the face
            // We do not render the face if this is the case. This is called "Backface Culling"
            if (intensity < 0)
            {
                return;
            } 
            
            Color faceColor = _colorDrawStrategy.GetColor();
            
            // Adjust the face's color by the intensity of the light hitting the surface
            // The more "direct" the light hits the surface, the more intense the color
            faceColor.SetIntensity(intensity);
            
            Triangle faceTriangle = TriangleHelper.TriangleFromFace(face, width, height);
            BoundingBox bb = BoundingBoxHelper.GetBoundingBox(faceTriangle.Points);
            
            bb.PerformActionOverBoundingBox(((point) =>
            {
                if (TriangleHelper.PointInTriangle(faceTriangle, point))
                {
                    Point drawPoint = PointHelper.OffsetPoint(point, start);
                    pixelBuffer.SetPixel(drawPoint.X, drawPoint.Y, faceColor);
                }
            }));
        }
    }
}