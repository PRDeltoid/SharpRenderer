using System;
using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class RandomColorDrawStrategy : IColorDrawStrategy
    {
        private static readonly Random Rand = new Random();

        public bool ColorPerFace { get; }
        
        public RandomColorDrawStrategy(bool perFace = true)
        {
            // RandomColor is *generally* per-face, but can be per-pixel for fun "static" pattern results
            ColorPerFace = perFace;
        }

        public Color GetColor(Face face, Vector3 barycentricCoord)
        {
            return GenerateRandomColor();
        }

        private static Color GenerateRandomColor()
        {
            return Color.FromArgb(Rand.Next(0, 256), 
                Rand.Next(0, 256), Rand.Next(0, 256));  
        }
    }
}