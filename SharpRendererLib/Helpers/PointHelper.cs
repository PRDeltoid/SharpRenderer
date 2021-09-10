using System.Drawing;

namespace SharpRendererLib.Helpers
{
    public static class PointHelper
    {
        public static bool IsOutOfBounds(int width, int height, Point point)
        {
            return point.X >= width ||
                   point.Y >= height;
        }

        public static void SwapPoints(ref Point point1, ref Point point2)
        {
            (point1, point2) = (point2, point1);
        }

        public static void TransposeXY(ref Point point1)
        {
            // Swap X and Y values 
            // This can be reversed by calling this function again
            (point1.X, point1.Y) = (point1.Y, point1.X);
        }

        public static double CalculateSlope(Point point1, Point point2)
        {
            //Special case: A horizontal line has zero slope
            if (point1.Y == point2.Y) return 0;
            //Special case: A vertical line has infinite slope
            if (point1.X == point2.X) return double.PositiveInfinity;
            
            return (point2.Y - point1.Y) / (double)(point2.X - point1.X);
        }

        public static int DistanceX(Point point1, Point point2)
        {
            return point2.X - point1.X;
        }
        
        public static int DistanceY(Point point1, Point point2)
        {
            return point2.Y - point1.Y;
        }
    }
}