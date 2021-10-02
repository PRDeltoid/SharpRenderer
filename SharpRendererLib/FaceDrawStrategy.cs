using System.Drawing;
using System.Numerics;
using SharpRendererLib.Extensions;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly IColorDrawStrategy _colorDrawStrategy;
        private readonly ViewPort _viewPort;
        private readonly Matrix _projection;
        private readonly IShadingStrategy _shadingStrategy;

        public FaceDrawStrategy(IColorDrawStrategy colorDrawStrategy, IShadingStrategy shadingStrategy, Camera camera, ViewPort viewPort, Matrix modelView)
        {
            _colorDrawStrategy = colorDrawStrategy;
            _shadingStrategy = shadingStrategy;
            _projection = Matrix.Identity(4);
            _projection[3, 2] = -1f / camera.Z;
            _viewPort = viewPort;
            ModelView = modelView;
        }
       
        public void DrawFace(PixelBuffer pixelBuffer, Face face, Light light, ZBuffer zBuffer, int width, int height,
            Point start)
        {
            // Project the face onto our viewport
            Triangle faceTriangle = TriangleHelper.TriangleFromFace(face, _viewPort, _projection, ModelView);
            // Find the face's screen triangle's bounding box on the screen
            BoundingBox bb = BoundingBoxHelper.GetBoundingBox(faceTriangle.Points);

            Color faceColor = Color.Aqua;
            if (_colorDrawStrategy.ColorPerFace)
            {
                faceColor = _colorDrawStrategy.GetColor(face, Vector3.Zero);
            }
            
            // If we are shading the face using a per-face strategy, generate the intensity value for this face
            float faceIntensity = 0;
            if (_shadingStrategy.ShadePerFace)
            {
                float intensity = _shadingStrategy.GetIntensity(face, Vector3.Zero, light);
                // If the intensity is negative, the light is coming from behind the face
                // We do not render the face if this is the case. This is called "Backface Culling"
                if (intensity < 0)
                {
                    return;
                }

                faceIntensity = intensity;

                // If we are coloring per face, save some time and calculate the shaded color value
                // only once
                if (_colorDrawStrategy.ColorPerFace)
                {
                    faceColor.SetIntensity(intensity);
                }
            }

            if (faceIntensity < 0)
            {
                return;
                
            }

            bb.PerformActionOverBoundingBox(((point) =>
            {
                // Check if our barycentric coordinate indicates that the point is within the bounds of faceTriangle
                // If it is out of bounds, skip drawing it.
                Vector3 bc = TriangleHelper.Barycentric(faceTriangle, point);
                if (BarycentricPointInTriangle(bc) == false) return;

                // If we've already drawn something closer to the camera already, skip drawing this point
                float z = CalculatePointZ(face, bc);
                if (zBuffer.TrySetZ(point.X, point.Y, z) == false) return;

                Color pointColor = faceColor;
                if (_colorDrawStrategy.ColorPerFace && _shadingStrategy.ShadePerFace == false)
                {
                    pointColor.SetIntensity(_shadingStrategy.GetIntensity(face, bc, light));
                }
                else if (_colorDrawStrategy.ColorPerFace == false && _shadingStrategy.ShadePerFace)
                {
                    pointColor = GetPointColor(face, bc);
                    pointColor.SetIntensity(faceIntensity);
                }
                else if (_colorDrawStrategy.ColorPerFace == false && _shadingStrategy.ShadePerFace == false)
                {
                    float intensity = _shadingStrategy.GetIntensity(face, bc, light);
                    pointColor = GetPointColor(face, bc);
                    pointColor.SetIntensity(intensity);
                }

                // If we get here, we've passed all of our tests and should draw the point
                pixelBuffer.SetPixel(point.X, point.Y, pointColor);
            }));
        }

        private Color GetPointColor(Face face, Vector3 barycentricCoordinates)
        {
            return _colorDrawStrategy.GetColor(face, barycentricCoordinates);
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