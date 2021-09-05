using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class BresenhamLineDrawStrategyTests
    {
        [Test]
        [TestCase(1, 1, 5, 5, 3, 3)]
        public void DetermineY_ReturnsExpectedValue(int x1, int y1, int x2, int y2, int testX, int expectedY)
        {
            Point point1 = new(x1, y1);
            Point point2 = new(x2, y2);
            Line line = new Line(point1, point2);
            BresenhamLineDrawStrategy drawStrat = new();
            //TODO: Make a meaningful test here
        }
    }
}