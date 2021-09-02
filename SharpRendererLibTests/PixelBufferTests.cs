using System;
using System.Drawing;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    public class PixelBufferTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SetPixel_InBounds_ShouldNotThrow()
        {
            PixelBuffer pixelBuffer = new(10,10);
            pixelBuffer.SetPixel(0, 0, Color.Aqua);
            Assert.Pass();
        }
        
        [Test]
        public void SetPixel_OutOfBounds_ShouldThrow()
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.Throws<Exception>(() => pixelBuffer.SetPixel(11, 11, Color.Aqua));
        }
        
        [Test]
        [TestCase(11,11)]
        [TestCase(10,10)]
        public void GetPixel_OutOfBounds_ShouldThrow(int x, int y)
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.Throws<Exception>(() => pixelBuffer.GetPixel(x, y));
        }
        
        [Test]
        public void GetPixel_OnNewBuffer_ShouldReturnWhite()
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.AreEqual(Color.White, pixelBuffer.GetPixel(0, 0));
        }
    }
}