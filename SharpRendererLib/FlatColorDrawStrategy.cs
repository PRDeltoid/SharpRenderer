using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FlatColorDrawStrategy : IColorDrawStrategy
    {
        private readonly Color _color;

        public bool ColorPerFace { get; }
        public FlatColorDrawStrategy(Color color)
        {
            _color = color;
            // Flat color draw strategy is always per-face color
            ColorPerFace = true;
        }

        public Color GetColor(Face face, Vector3 barycentricCoord)
        {
            return _color;
        }

    }
}