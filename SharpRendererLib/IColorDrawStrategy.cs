using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IColorDrawStrategy
    {
        Color GetColor(Face face, Vector3 barycentricCoord);
        bool ColorPerFace { get; }
    }
}