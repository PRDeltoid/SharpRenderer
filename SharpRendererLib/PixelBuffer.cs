using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class PixelBuffer
    {
        private Color[,] Pixels { get; } 
        public int Height { get; }
        public int Width { get; }

        /// <summary>
        /// A pixel buffer which represents colors-per-pixel in a 2D space
        /// </summary>
        /// <param name="width">The width of the pixel buffer. Max X value = Width - 1</param>
        /// <param name="height">The height of the pixel buffer. Max Y value = Height - 1</param>
        public PixelBuffer(int width, int height)
        {
            Pixels = new Color[width, height];
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Sets a pixel's color at a given coordinate
        /// Coordinates are zero-based
        /// </summary>
        /// <param name="x">A zero-based value for how far from the left-hand side of the pixel buffer we want to draw</param>
        /// <param name="y">A zero-based value for how far from the top-side of the pixel buffer we want to draw</param>
        /// <param name="color">The color we want to paint the pixel</param>
        /// <exception cref="Exception">Exception thrown if <paramref name="x"/> or <paramref name="y"/> are out of the pixel buffers boundaries (Width-1 and Height-1 respectively)</exception>
        public void SetPixel(int x, int y, Color color)
        {
            if (x >= Width || y >= Height) throw new Exception($"Attempt to write pixel outside of PixelBuffer boundaries W: {Width} H: {Height}");
            Pixels[x,y] = color;
        }

        /// <summary>
        /// Gets a pixel's color at a given coordinate
        /// Coordinates are zero-based
        /// </summary>
        /// <param name="x">A zero-based value for how far from the left-hand side of the pixel buffer we want to poll for a color</param>
        /// <param name="y">A zero-based value for how far from the top-side of the pixel buffer we want to poll for a color</param>
        /// <returns>The color value at <paramref name="x"/>,<paramref name="y"/></returns>
        /// <exception cref="Exception">Exception thrown if <paramref name="x"/> or <paramref name="y"/> are out of the pixel buffers boundaries</exception>
        public Color GetPixel(int x, int y)
        {
            if (x >= Width || y >= Height) throw new Exception($"Attempt to get pixel outside of PixelBuffer boundaries W: {Width} H: {Height}");
            return Pixels[x, y];
        }
    }
}