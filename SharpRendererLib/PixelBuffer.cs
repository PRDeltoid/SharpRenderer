using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class PixelBuffer
    {
        private Color[,] Pixels { get; } 
        public int Height { get; }
        public int Width { get; }

        public PixelBuffer(int width, int height)
        {
            Pixels = new Color[width, height];
            Height = height;
            Width = width;
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= Width || y >= Height) throw new Exception($"Attempt to write pixel outside of PixelBuffer boundaries W: {Width} H: {Height}");
            Pixels[x,y] = color;
        }

        public Color GetPixel(int x, int y)
        {
            if (x >= Width || y >= Height) throw new Exception($"Attempt to get pixel outside of PixelBuffer boundaries W: {Width} H: {Height}");
            return Pixels[x, y];
        }
    }
}