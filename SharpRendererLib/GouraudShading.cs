using System;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class GouraudShading : IShadingStrategy
    {
        private readonly NormalMap _normalMap;

        public GouraudShading(NormalMap normalMap)
        {
            _normalMap = normalMap;
        }
        
        public float GetIntensity(Face face, Vector3 barycentricCoord, Light light)
        {
            return GetIntensityForPoint(face, barycentricCoord, light); 
        }

        private float GetIntensityForPoint(Face face, Vector3 barycentric, Light light)
        {
            float intensity = 0;
            
            // Determine the intensity of light hitting each vertex in the face
            Vector3 pointNormal = GetNormalForPoint(face, barycentric);
            float vert1Intensity = Math.Max(0, Vector3.Dot(pointNormal, light));
            float vert2Intensity = Math.Max(0, Vector3.Dot(pointNormal, light));
            float vert3Intensity = Math.Max(0, Vector3.Dot(pointNormal, light));
            
            // Interpolate the vertexes intensities using the barycentric coordinates of the point we are shading 
            intensity += vert1Intensity * barycentric.X;
            intensity += vert2Intensity * barycentric.Y;
            intensity += vert3Intensity * barycentric.Z;

            return intensity;
        }
        
        private Vector3 GetNormalForPoint(Face face, Vector3 barycentric)
        {
            float x = 0;
            
            // Interpolate Z value of the current point we are drawing using its barycentric coordinates
            x += face.TextureVertex1.U * barycentric.X;
            x += face.TextureVertex2.U * barycentric.Y;
            x += face.TextureVertex3.U * barycentric.Z;
            
            float y = 0;
            
            // Interpolate Z value of the current point we are drawing using its barycentric coordinates
            y += face.TextureVertex1.V * barycentric.X;
            y += face.TextureVertex2.V * barycentric.Y;
            y += face.TextureVertex3.V * barycentric.Z;
            return _normalMap.GetPointNormal((int)(x * _normalMap.Width), (int)(y * _normalMap.Height));
        }
    }
}