using System;
using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;
using SharpRendererLib.Models;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class LineTests
    {
        [Test]
        [TestCase(3, 1, 1, 1)]
        [TestCase(4, 1, 1, 1)]
        [TestCase(5, 1, 1, 1)]
        [TestCase(5, 1, 5, 1)]
        public void LineCtor_SwapsPoints_WhenPointsAreRightToLeft(int x1, int y1, int x2, int y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Line line = new Line(new Point(x1, y1), new Point(x2, y2));
            Assert.AreEqual(p2, line.Point1);
            Assert.AreEqual(p1, line.Point2);
        }
    }
}