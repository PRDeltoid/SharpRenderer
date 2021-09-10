using System;
using System.Drawing;
using SharpRendererLib.Helpers;

namespace SharpRendererLib
{
    public class LineDrawer
    {
        private readonly ILineDrawStrategy _drawStrategy;

        public LineDrawer(ILineDrawStrategy drawStrategy)
        {
            _drawStrategy = drawStrategy ?? throw new ArgumentNullException(nameof(drawStrategy));
        }

        public void DrawLine(PixelBuffer pixelBuffer, Line line, Color color)
        {
            int width = pixelBuffer.Width;
            int height = pixelBuffer.Height;
            
            if (PointHelper.IsOutOfBounds(width, height, line.Point1) ||
                PointHelper.IsOutOfBounds(width, height, line.Point2))
            {
                throw new Exception($"One of the provided points is out of bounds. Boundary: ({width-1}, {height-1}). Line goes from ({line.Point1.X}, {line.Point1.Y}) to ({line.Point2.X}, {line.Point2.Y}).");
            }
            
            // Let our line draw strat determine where to place pixels
            _drawStrategy.CallOnAllDrawPoints(line, (drawX, drawY) => 
                    pixelBuffer.SetPixel(drawX, drawY, color));
        }
    }
}