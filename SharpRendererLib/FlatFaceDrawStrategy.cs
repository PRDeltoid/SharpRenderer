using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using SharpRendererLib.Extensions;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;
using Face = SharpRendererLib.Models.Face;

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
                // If where we're drawing is negative, skip it as it is outside the screen
                Point drawPoint = PointHelper.OffsetPoint(point, start);
                if(drawPoint.X < 0 || drawPoint.Y < 0 || drawPoint.X >= width+start.X || drawPoint.Y >= height+start.Y) return;
                
                // Check if our barycentric coordinate indicates that the point is within the bounds of faceTriangle
                // If it is out of bounds, skip drawing it.
                Vector3 bc = TriangleHelper.Barycentric(faceTriangle, point);
                if (BarycentricPointInTriangle(bc) == false) return;
                
                // If we've already drawn something closer to the camera already, skip drawing this point
                float z = CalculatePointZ(face, bc);
                if (zBuffer.TrySetZ(drawPoint.X, drawPoint.Y, z) == false) return;

                // If we get here, we've passed all of our tests and should draw the point
                pixelBuffer.SetPixel(drawPoint.X, drawPoint.Y, faceColor);
            }));
        }

        private static bool BarycentricPointInTriangle(Vector3 barycentricPoint)
        {
            if (barycentricPoint.X < 0 || barycentricPoint.Y < 0 || barycentricPoint.Z < 0) return false;

            return true; 
        }

        private static float CalculatePointZ(Face face, Vector3 barycentric)
        {
            float z = 0;
            // Interpolate Z value of the current point we are drawing using its barycentric coordinates
            z += face.Vertex1.Z * barycentric.X;
            z += face.Vertex2.Z * barycentric.Y;
            z += face.Vertex3.Z * barycentric.Z;
            return z;
        }
    }
}