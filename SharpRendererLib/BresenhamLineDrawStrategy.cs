using System;

namespace SharpRendererLib
{
    public class BresenhamLineDrawStrategy : ILineDrawStrategy
    {
        public int DetermineY(int x, Line line)
        {
            double slope = PointHelper.CalculateSlope(point1, point2);
            double yValue = slope * (x - point1.X) + (point1.Y);
            return Convert.ToInt32(yValue);
        }

        public void Initialize()
        {
            _isNewLine = true;
        }
    }
}