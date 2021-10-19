using System.Numerics;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FlatShadingStrategy : IShadingStrategy
    {
        public float GetIntensity(Face face, Vector3 _barycentricCoord, Light light)
        {
            // Get a vector that is normal (orthagonal) to the face
            Vector3 faceNormal = FaceHelper.GetFaceNormal(face);
            
            // Determine the intensity of light hitting the face
            float intensity = Vector3.Dot(faceNormal, light);
            return intensity;
        }
    }
}