using System.Drawing;

namespace SharpRendererLib
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
    }
}