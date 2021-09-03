﻿using System;
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

        public void DrawLine(PixelBuffer pixelBuffer, Point point1, Point point2, Color color)
        {
            int width = pixelBuffer.Width;
            int height = pixelBuffer.Height;
           
            if (point1 == point2)
            {
                throw new Exception("Failed to draw line. Both points are the same.");
            } 
            
            if (PointHelper.IsOutOfBounds(width, height, point1) ||
                PointHelper.IsOutOfBounds(width, height, point2))
            {
                throw new Exception("One of the provided points is out of bounds");
            }

            //Always draw left-to-right (smallest X to largest X)
            // Since we always draw from point1 -> point2, this ensures that point1 is always further left
            if (point1.X > point2.X)
            {
                PointHelper.SwapPoints(ref point1, ref point2);
            }

            for (int drawX = point1.X; drawX < point2.X; drawX++)
            {
                int drawY = _drawStrategy.DetermineY(drawX, point1, point2);
                pixelBuffer.SetPixel(drawX, drawY, color);
            }
        }
    }
}