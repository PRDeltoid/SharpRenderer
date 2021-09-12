using System.Drawing;
using NUnit.Framework;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;
using Face = SharpRendererLib.Models.Face;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class TriangleHelperTests
    {
        [Test]
        public void TriangleFromFace_ReturnsCorrectValues()
        {
            Face face = new Face(new Vertex(0,0,0), new Vertex(1,1,0), new Vertex(-1, 0, 0));
            Triangle triangle = TriangleHelper.TriangleFromFace(face, 10, 10);
            Assert.AreEqual(0, triangle.Point1.X);
            Assert.AreEqual(0, triangle.Point1.Y);
            
            Assert.AreEqual(5, triangle.Point2.X);
            Assert.AreEqual(5, triangle.Point2.Y);
            
            Assert.AreEqual(5, triangle.Point3.X);
            Assert.AreEqual(0, triangle.Point3.Y);
        }

        [Test]
        public void PointInTriangle_ReturnsCorrectValues()
        {
            Triangle triangle = new Triangle(new Point(0, 0), new Point(0, 10), new Point(10, 10));
            Assert.IsTrue(TriangleHelper.PointInTriangle(triangle, new Point(5, 5)));
            Assert.IsFalse(TriangleHelper.PointInTriangle(triangle, new Point(10, 0)));
            
        }
    }
}