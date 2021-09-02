using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class PixelBuffer
    {
        public Color[,] Pixels { get; set; }
        public int Height { get; internal set; }
        public int Width { get; internal set; }

        public PixelBuffer(int width, int height)
        {
            Pixels = new Color[width,height];
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x > Width || y > Height) throw new Exception($"Attempt to write outside of PixelBuffer boundaries W: {Width} H: {Height}");
            Pixels[x,y] = color;
        }

        public Color GetPixel(int x, int y)
        {
            if (x > Width || y > Height) throw new Exception($"Attempt to write outside of PixelBuffer boundaries W: {Width} H: {Height}");
            return Color.White;
        }
    }
}