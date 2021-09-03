using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class PointHelperTests
    {
        [Test]
        [TestCase(10, 10, 11, 11)]
        [TestCase(10, 10, 10, 10)]
        [TestCase(10, 11, 11, 1)]
        [TestCase(10, 10, 1, 11)]
        [TestCase(10, 10, 10, 1)]
        [TestCase(10, 10, 1, 10)]
        public void IsOutOfBounds_PointOutOfBounds_ReturnsTrue(int width, int height, int x, int y)
        {
            Point point = new Point(x, y);
            Assert.IsTrue(PointHelper.IsOutOfBounds(width, height, point));
        }
        
        [Test]
        [TestCase(1, 1, 2, 2)]
        [TestCase(2, 2, 1, 1)]
        public void SwapPoints_ValuesAreSwapped(int x1, int y1, int x2, int y2)
        {
            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x2, y2);
            
            PointHelper.SwapPoints(ref point1, ref point2);
            Assert.AreEqual(x1, point2.X);
            Assert.AreEqual(y1, point2.Y);
            Assert.AreEqual(x2, point1.X);
            Assert.AreEqual(y2, point1.Y);
        }

        [Test]
        [TestCase(1, 1, 3, 1, 0)]
        [TestCase(1, 1, 1, 1, 0)]
        [TestCase(1, 1, 1, 3, double.PositiveInfinity)]
        [TestCase(1, 1, 2, 2, 1)]
        [TestCase(2, 1, 1, 2, -1)]
        [TestCase(1, 2, 2, 1, -1)]
        [TestCase(1, 1, 2, 3, 2)]
        [TestCase(2, 1, 1, 3, -2)]
        [TestCase(1, 3, 2, 1, -2)]
        [TestCase(1, 1, 3, 2, 0.5)]
        [TestCase(1, 2, 3, 1, -0.5)]
        [TestCase(3, 1, 1, 2, -0.5)]
        [TestCase(1, 1, 5, 2, 0.25)]
        [TestCase(1, 2, 5, 1, -0.25)]
        [TestCase(5, 1, 1, 2, -0.25)]
        [TestCase(1, 1, 101, 2, 0.01)]
        [TestCase(1, 2, 101, 1, -0.01)]
        [TestCase(101, 1, 1, 2, -0.01)]
        public void CalculateSlope_ReturnsCorrectValue(int x1, int y1, int x2, int y2, double expectedSlope)
        {
            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x2, y2);

            Assert.AreEqual(expectedSlope, PointHelper.CalculateSlope(point1, point2));
        }
    }
}