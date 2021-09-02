using System.Diagnostics;
using System.Drawing;
using System.IO;
using SharpRendererLib;

namespace SharpRendererConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 10;
            const int height = 10;
            PixelBuffer pixelBuff = new PixelBuffer(width, height);
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if(x==y) pixelBuff.SetPixel(x,y,Color.Aqua);
                } 
            }

            Bitmap bitmap = Draw(pixelBuff);
            string outPath = Path.Combine(Path.GetTempPath(), "test.bmp");
            bitmap.Save(outPath);
            OpenImage(outPath);
        }

        private static void OpenImage(string imagePath)
        {
            ProcessStartInfo openImageProc = new(imagePath) { UseShellExecute = true };
            Process.Start(openImageProc);
        }

        private static Bitmap Draw(PixelBuffer pixelBuffer)
        {
            Bitmap bitmap = new(pixelBuffer.Width, pixelBuffer.Height);

            for (int x = 0; x < pixelBuffer.Width; x++)
            {
                for (int y = 0; y < pixelBuffer.Height; y++)
                {
                    bitmap.SetPixel(x, y, pixelBuffer.GetPixel(x,y));
                }
            }

            return bitmap;
        }
    }
}