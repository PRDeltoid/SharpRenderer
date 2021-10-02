using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IShadingStrategy
    {
        float GetIntensity(Face face, Vector3 barycentricCoord, Light light);
        bool ShadePerFace { get; }
    }
}