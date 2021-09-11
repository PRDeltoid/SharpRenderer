using System.Drawing;

namespace SharpRendererLib.Models
{
    public class Triangle
    {
        public Point Point1 { get; }
        public Point Point2 { get; }
        public Point Point3 { get; }

        public Point[] Points { get; }

        public Triangle(Point point1, Point point2, Point point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Points = new[] { Point1, Point2, Point3 };
        }
    }
}