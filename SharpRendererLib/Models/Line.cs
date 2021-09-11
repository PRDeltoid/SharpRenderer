using System.Drawing;
using SharpRendererLib.Helpers;

namespace SharpRendererLib.Models
{
    public class Line
    {
        public double Slope { get; }
        public Point Point1 { get; }
        public Point Point2 { get; }

        public Line(Point point1, Point point2)
        {
            // Our line object always holds our line in left-to-right orientation (smallest X to largest X)
            if (point1.X > point2.X)
            {
                // This swap ensures that point1 is always further left
                PointHelper.SwapPoints(ref point1, ref point2);
            }

            Point1 = point1;
            Point2 = point2;
            
            Slope = PointHelper.CalculateSlope(point1, point2);
        }
    }
}