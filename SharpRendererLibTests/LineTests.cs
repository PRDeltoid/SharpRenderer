using System;
using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class LineTests
    {
        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(3, 3, 3, 3)]
        public void ThrowsWhen_PointsAreTheSame(int x1, int y1, int x2, int y2)
        {
            Assert.Throws<Exception>(() => new Line(new Point(x1, x1), new Point(x2, y2)));
        }
    }
}