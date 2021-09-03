using System;
using System.Drawing;

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
                throw new Exception("One of the provided points is out of bounds");
            }

            for (int drawX = line.Point1.X; drawX < line.Point2.X; drawX++)
            {
                int drawY = _drawStrategy.DetermineY(drawX, line);
                pixelBuffer.SetPixel(drawX, drawY, color);
            }
        }
    }
}