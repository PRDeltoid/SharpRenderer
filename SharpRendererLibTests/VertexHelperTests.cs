using System.Drawing;
using NUnit.Framework;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class VertexHelperTests
    {
        [Test]
        [TestCase(1, 1, 10, 10, 5, 5)]
        [TestCase(2, 2, 10, 10, 10, 10)]
        [TestCase(5, 5, 10, 10, 25, 25)]
        public void VertexToPoint_ReturnsCorrectValues (float vx, float vy, int width, int height, int expectedX, int expectedY)
        {
            Vertex vert = new Vertex(vx, vy, 0);

            Point point = VertexHelper.VertexToPoint(vert, width / 2, height / 2);

            Assert.AreEqual(expectedX, point.X); 
            Assert.AreEqual(expectedY, point.Y); 
        }
    }
}