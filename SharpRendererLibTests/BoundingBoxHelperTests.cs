using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;
using SharpRendererLib.Helpers;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class BoundingBoxHelperTests
    {
        [Test]
        [TestCase(10, 10, 20, 20)]
        [TestCase(20, 20, 10, 10)]
        [TestCase(20, 20, 20, 20)]
        [TestCase(10, 20, 20, 10)]
        [TestCase(20, 10, 10, 20)]
        public void GetBoundingBox_ReturnsCorrectValues(int point1X, int point1Y, int point2X, int point2Y)
        {
            // Calculate our expected values 
            int expectedUpperX = point1X > point2X ? point1X : point2X;
            int expectedLowerX = point1X < point2X ? point1X : point2X;
            int expectedUpperY = point1Y > point2Y ? point1Y : point2Y;
            int expectedLowerY = point1Y < point2Y ? point1Y : point2Y;
            
            Point point1 = new Point(point1X, point1Y);
            Point point2 = new Point(point2X, point2Y);
            Point[] points = { point1, point2 };
            BoundingBox boundingBox = BoundingBoxHelper.GetBoundingBox(points);
            
            Assert.AreEqual(expectedLowerX, boundingBox.TopLeft.X);
            Assert.AreEqual(expectedLowerY, boundingBox.TopLeft.Y);
            
            Assert.AreEqual(expectedUpperX, boundingBox.BottomRight.X);
            Assert.AreEqual(expectedUpperY, boundingBox.BottomRight.Y);
        }
    }
}