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
        [TestCase(11,11)]
        [TestCase(10,10)]
        public void SetPixel_OutOfBounds_ShouldThrow(int x, int y)
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.Throws<Exception>(() => pixelBuffer.SetPixel(x, y, Color.Aqua));
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
        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(8,8)]
        [TestCase(9,9)]
        public void GetPixel_OnNewBuffer_ShouldReturnWhite(int x, int y)
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.AreEqual(Color.Empty, pixelBuffer.GetPixel(x, y));
        }
        
        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(8,8)]
        [TestCase(9,9)] 
        public void GetPixel_ShouldReturn_SetPixelColor(int x, int y)
        {
            PixelBuffer pixelBuffer = new(10,10);
            Assert.AreEqual(Color.Empty, pixelBuffer.GetPixel(x, y));
            pixelBuffer.SetPixel(x, y, Color.Aqua);
            Assert.AreEqual(Color.Aqua, pixelBuffer.GetPixel(x, y));
        }
    }
}