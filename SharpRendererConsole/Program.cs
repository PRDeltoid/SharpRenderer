using System.Diagnostics;
using System.Drawing;
using System.IO;
using SharpRendererLib;

namespace SharpRendererConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            const int width = 90;
            const int height = 90;
            PixelBuffer pixelBuff = new PixelBuffer(width, height);
            LineDrawer lineDrawer = new LineDrawer(new BresenhamLineDrawStrategy());
            
            lineDrawer.DrawLine(pixelBuff, new Point(13,20), new Point(80,40), Color.White);
            lineDrawer.DrawLine(pixelBuff, new Point(20,13), new Point(40,80), Color.Magenta);
            lineDrawer.DrawLine(pixelBuff, new Point(80,40), new Point(13,20), Color.Aqua);

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