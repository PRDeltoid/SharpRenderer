using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Pfim;
using ImageFormat = Pfim.ImageFormat;

namespace SharpRendererLib
{
    public static class TgaFileParser
    {
        public static Bitmap ParseFile(string path)
        {
            IImage image = Pfim.Pfim.FromFile(path);
            PixelFormat format;
            // Convert from Pfim's backend agnostic image format into GDI+'s image format
            switch (image.Format)
            {
                case ImageFormat.Rgba32:
                    format = PixelFormat.Format32bppArgb;
                    break;
                case ImageFormat.Rgb24:
                    format = PixelFormat.Format24bppRgb;
                    break;
                default:
                    // see the sample for more details
                    throw new NotImplementedException(); 
            } 
            var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            return new Bitmap(image.Width, image.Height, image.Stride, format, data);
        }
    }
}