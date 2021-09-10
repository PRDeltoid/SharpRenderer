using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class RandomColorDrawStrategy : IFlatColorDrawStrategy
    {
        private static readonly Random Rand = new Random();
        
        public Color GetColor()
        {
            return Color.FromArgb(Rand.Next(0, 256), 
                Rand.Next(0, 256), Rand.Next(0, 256));
        }
    }
}