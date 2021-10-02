using System.Drawing;
using System.Numerics;
using SharpRendererLib.Extensions;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FaceDrawStrategy : FaceDrawStrategyBase, IFaceDrawStrategy
    {
        private readonly IColorDrawStrategy _colorDrawStrategy;
        private readonly IShadingStrategy _shadingStrategy;

        public FaceDrawStrategy(IColorDrawStrategy colorDrawStrategy, IShadingStrategy shadingStrategy, Camera camera, ViewPort viewPort, ModelView modelView)
            : base(camera, viewPort, modelView)
        {
            _colorDrawStrategy = colorDrawStrategy;
            _shadingStrategy = shadingStrategy;
        }

        public void DrawFace(PixelBuffer pixelBuffer, Face face, Light light, ZBuffer zBuffer)
        {
            // Project the face onto our viewport
            Triangle faceTriangle = TriangleHelper.TriangleFromFace(face, TransformationMatrix);
            // Find the face's screen triangle's bounding box on the screen
            BoundingBox bb = BoundingBoxHelper.GetBoundingBox(faceTriangle.Points);

            // If the color or shading strategy is per-face, cache the value for use later
            // If the face should not be displayed because no light is hitting it, return early
            Color faceColor = Color.Aqua;
            float faceIntensity = 0;
            if (DetermineFaceColorAndShading(face, light, ref faceColor, ref faceIntensity)) return;

            bb.PerformActionOverBoundingBox(((point) =>
            {
                // Check if our barycentric coordinate indicates that the point is within the bounds of faceTriangle
                // If it is out of bounds, skip drawing it.
                Vector3 bc = TriangleHelper.Barycentric(faceTriangle, point);
                if (BarycentricPointInTriangle(bc) == false) return;

                // If we've already drawn something closer to the camera already, skip drawing this point
                float z = CalculatePointZ(face, bc);
                if (zBuffer.TrySetZ(point.X, point.Y, z) == false) return;

                Color pointColor = CalculatePointColor(face, light, faceColor, bc, faceIntensity);
                pixelBuffer.SetPixel(point.X, point.Y, pointColor);
            }));
        }

        private bool DetermineFaceColorAndShading(Face face, Light light, ref Color faceColor, ref float faceIntensity)
        {
            if (_colorDrawStrategy.ColorPerFace)
            {
                faceColor = _colorDrawStrategy.GetColor(face, Vector3.Zero);
            }

            // If we are shading the face using a per-face strategy, generate the intensity value for this face
            if (_shadingStrategy.ShadePerFace)
            {
                float intensity = _shadingStrategy.GetIntensity(face, Vector3.Zero, light);
                // If the intensity is negative, the light is coming from behind the face
                // We do not render the face if this is the case. This is called "Backface Culling"
                if (intensity < 0)
                {
                    return true;
                }

                faceIntensity = intensity;
                if (faceIntensity < 0)
                {
                    return false;
                }

                // If we are coloring per face, save some time and calculate the shaded color value
                // only once
                if (_colorDrawStrategy.ColorPerFace)
                {
                    faceColor.SetIntensity(intensity);
                }
            }

            return false;
        }

        private Color CalculatePointColor(Face face, Light light, Color faceColor, Vector3 bc, float faceIntensity)
        {
            Color pointColor = faceColor;
            if (_colorDrawStrategy.ColorPerFace && _shadingStrategy.ShadePerFace == false)
            {
                pointColor.SetIntensity(_shadingStrategy.GetIntensity(face, bc, light));
            }
            else if (_colorDrawStrategy.ColorPerFace == false && _shadingStrategy.ShadePerFace)
            {
                pointColor = _colorDrawStrategy.GetColor(face, bc);
                pointColor.SetIntensity(faceIntensity);
            }
            else if (_colorDrawStrategy.ColorPerFace == false && _shadingStrategy.ShadePerFace == false)
            {
                float intensity = _shadingStrategy.GetIntensity(face, bc, light);
                pointColor = _colorDrawStrategy.GetColor(face, bc);
                pointColor.SetIntensity(intensity);
            }

            return pointColor;
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