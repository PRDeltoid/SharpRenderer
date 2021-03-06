using System;
using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;
using SharpRendererLib.Models;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class LineDrawerTests
    {
        [Test]
        [TestCase(1, 1, 11, 11)]
        [TestCase(11, 11, 1, 1)]
        [TestCase(10, 10, 1, 1)]
        [TestCase(1, 1, 10, 10)]
        public void ThrowsWhen_EitherPointOutOfBounds(int x1, int y1, int x2, int y2)
        {
            LineDrawer lineDrawer = new(new DrawStrategyStub());
            Line line = new Line(new Point(x1, x1), new Point(x2, y2));
            
            Assert.Throws<Exception>(() => lineDrawer.DrawLine(new PixelBuffer(10, 10), line, Color.Empty));
        }
        
        [Test]
        [TestCase(9, 9, 1, 1)]
        [TestCase(9, 1, 9, 1)]
        [TestCase(1, 9, 1, 9)]
        [TestCase(1, 1, 9, 9)]
        public void DoesNotThrow_WhenPointsInBounds(int x1, int y1, int x2, int y2)
        {
            LineDrawer lineDrawer = new(new DrawStrategyStub());
            Line line = new Line(new Point(x1, x1), new Point(x2, y2));
            
            lineDrawer.DrawLine(new PixelBuffer(10, 10), line, Color.Empty);
        }
        
        private class DrawStrategyStub : ILineDrawStrategy
        {
            public void CallOnAllDrawPoints(Line line, Action<int, int> drawFunc)
            {
            }
        }
    }


}