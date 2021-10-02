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
            
            // Determine the intensity of light hitting the face
            float vert1intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex1Norm.X, face.Vertex1Norm.Y, face.Vertex1Norm.Z), light));
            float vert2intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex2Norm.X, face.Vertex2Norm.Y, face.Vertex2Norm.Z), light));
            float vert3intensity = Math.Max(0, Vector3.Dot(new Vector3(face.Vertex3Norm.X, face.Vertex3Norm.Y, face.Vertex3Norm.Z), light));
            
            intensity += vert1intensity * barycentric.X;
            intensity += vert2intensity * barycentric.Y;
            intensity += vert3intensity * barycentric.Z;

            return intensity;
        }
        
        public bool ShadePerFace => false;
    }
}