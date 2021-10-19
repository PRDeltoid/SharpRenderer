using System;
using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class RandomColorDrawStrategy : IColorDrawStrategy
    {
        private static readonly Random Rand = new Random();

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