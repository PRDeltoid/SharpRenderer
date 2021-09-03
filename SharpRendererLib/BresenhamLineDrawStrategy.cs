using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class BresenhamLineDrawStrategy : ILineDrawStrategy
    {
        public int DetermineY(int x, Point point1, Point point2)
        {
            double slope = PointHelper.CalculateSlope(point1, point2);
            double yValue = slope * (x - point1.X) + (point1.Y);
            return Convert.ToInt32(yValue);
        }
    }
}