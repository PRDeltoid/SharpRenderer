using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class BoundingBox
    {
        public BoundingBox(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Point TopLeft { get; }
        public Point BottomRight { get; }

        public void PerformActionOverBoundingBox(Action<int, int> action)
        {
            // Iterate over the entire bounding box
            for (int x = TopLeft.X; x < BottomRight.X; x++)
            for (int y = TopLeft.Y; y < BottomRight.Y; y++)
                // Run our provided function on our current point in the bounding box
                action.Invoke(x, y);
        }
    }
}