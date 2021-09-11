using System;
using System.Drawing;
using SharpRendererLib.Models;

namespace SharpRendererLib.Helpers
{
    public static class BoundingBoxHelper
    {
        public static BoundingBox GetBoundingBox(Point[] points)
        {
            int startX = int.MaxValue;
            int startY = int.MaxValue;
            int endX = 0;
            int endY = 0;

            foreach (Point point in points)
            {
                startX = Math.Min(point.X, startX);
                startY = Math.Min(point.Y, startY);
                endX = Math.Max(point.X, endX);
                endY = Math.Max(point.Y, endY);
            }

            return new BoundingBox(new Point(startX, startY), new Point(endX, endY));
        }
    }
}