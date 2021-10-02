using System.Drawing;
using NUnit.Framework;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class TriangleHelperTests
    {
        [Test]
        public void PointInTriangle_ReturnsCorrectValues()
        {
            Triangle triangle = new Triangle(new Point(0, 0), new Point(0, 10), new Point(10, 10));
            Assert.IsTrue(TriangleHelper.PointInTriangle(triangle, new Point(5, 5)));
            Assert.IsFalse(TriangleHelper.PointInTriangle(triangle, new Point(10, 0)));
            
        }
    }
}