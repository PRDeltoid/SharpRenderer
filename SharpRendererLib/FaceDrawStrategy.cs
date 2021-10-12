using System.Drawing;
using System.Numerics;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FaceDrawStrategy : FaceDrawStrategyBase, IFaceDrawStrategy
    {
        private readonly IShader _shader;

        public FaceDrawStrategy(IShader shader, Camera camera, ViewPort viewPort, ModelView modelView)
            : base(camera, viewPort, modelView)
        {
            _shader = shader;
        }

        public void DrawFace(PixelBuffer pixelBuffer, Face face, Light light, ZBuffer zBuffer)
        {
            // Project the face onto our viewport
            Triangle faceTriangle = TriangleHelper.TriangleFromFace(face, TransformationMatrix);
            // Find the face's screen triangle's bounding box on the screen
            BoundingBox bb = BoundingBoxHelper.GetBoundingBox(faceTriangle.Points);

            bb.PerformActionOverBoundingBox(((point) =>
            {
                // Check if our barycentric coordinate indicates that the point is within the bounds of faceTriangle
                // If it is out of bounds, skip drawing it.
                Vector3 bc = TriangleHelper.Barycentric(faceTriangle, point);
                if (BarycentricPointInTriangle(bc) == false) return;

                // If we've already drawn something closer to the camera already, skip drawing this point
                float z = CalculatePointZ(face, bc);
                if (zBuffer.TrySetZ(point.X, point.Y, z) == false) return;

                Color pointColor = CalculatePointColor(face, light, bc);
                pixelBuffer.SetPixel(point.X, point.Y, pointColor);
            }));
        }

        private Color CalculatePointColor(Face face, Light light, Vector3 bc)
        {
            return _shader.CalculatePointColor(face, light, bc);
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

    public interface IShader
    {
        Color CalculatePointColor(Face face, Light light, Vector3 barycentricCoordinates);
    }
}