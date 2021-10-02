using System;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class GouraudShading : IShadingStrategy
    {
        public float GetIntensity(Face face, Vector3 barycentricCoord, Light light)
        {
            return GetIntensityForPoint(face, barycentricCoord, light); 
        }

        private static float GetIntensityForPoint(Face face, Vector3 barycentric, Light light)
        {
            float intensity = 0;
            
            // Determine the intensity of light hitting each vertex in the face
            float vert1Intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex1Norm.X, face.Vertex1Norm.Y, face.Vertex1Norm.Z), light));
            float vert2Intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex2Norm.X, face.Vertex2Norm.Y, face.Vertex2Norm.Z), light));
            float vert3Intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex3Norm.X, face.Vertex3Norm.Y, face.Vertex3Norm.Z), light));
            
            // Interpolate the vertexes intensities using the barycentric coordinates of the point we are shading 
            intensity += vert1Intensity * barycentric.X;
            intensity += vert2Intensity * barycentric.Y;
            intensity += vert3Intensity * barycentric.Z;

            return intensity;
        }
        
        public bool ShadePerFace => false;
    }
}